window.addEventListener('load', function () {

    var btnSandesElem = document.getElementById('emailsent');
    var downloadBtnElem = document.getElementById('btndwonload');
    var emailBtnElem = document.getElementById('emailsent');
    const applicationNo = '1';
    const ipAddress = '1';
    var progressBarElem = document.getElementById('i-progress-inner');
    const emailAddress = document.getElementById('emailid').value;
    const isEmailIdVerified = "Y";
   
    const IsSandesActive = "";

    const fileName = "SaleInvoice";// document.getElementById('hidfilename').value;
    

    function MakeProgress(value, textLabel) {
        var label;
        label = '(' + value.toString() + '%)' + textLabel;
        $('.progress-bar')
            .css("width", value.toString() + "%")
            .attr('aria-valuenow', value)
            .text(label);
    }

  
    function getCurrentDate() {
        var today = new Date();
        var dd = today.getDate();

        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        var hours = today.getHours();
        var min = today.getMinutes();
        var sec = today.getSeconds();
        // Check whether AM or PM 
        var newformat = hours >= 12 ? 'PM' : 'AM';
        // Find current hour in AM-PM Format 
        hours = hours % 12;
        // To display "0" as "12" 
        hours = hours ? hours : 12;
        min = min < 10 ? '0' + min : min;
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        if (min < 10) {
            min = '0' + min;
        }
        if (hours < 10) {
            hours = '0' + hours;
        }
        if (sec < 10) {
            sec = '0' + sec;
        }
        today = dd + '/' + mm + '/' + yyyy + ' ' + hours + ':' + min + ':' + sec + ' ' + newformat;
        return today;
    }

    function getFooterText(PageNo, TotalPages) {
        var currentdate = getCurrentDate();
        var text = 'Receipt Number : ' + applicationNo + '   IP Address : ' + ipAddress +
            '                   Date and Time : ' + currentdate + '               [Page  ' + PageNo + ' of ' + TotalPages + ' ]';

        text = '';
        return text;
    }

    function downloadPdf(isSendEmailFlow) {

        progressBarElem.classList.remove('c-background--red');
        var progressLabel = 'Downloading';
        if (isSendEmailFlow === 1) {
            progressLabel = 'Emailing'
        }
        if (isSendEmailFlow === 2) {
            progressLabel = 'Downloading'
        }

        MakeProgress(20, progressLabel);
        
        var element = ''; element = document.getElementsByName('printable')[0];
        console.log('ggg');
        element.style.fontSize = "10px";

        var opt = {
            margin: [2, 2, 2, 2],
            filename: fileName,
            image: { type: 'jpeg', quality: 0.7 },
            html2canvas: { scale: 2, useCORS: true },
            jsPDF: { format: 'a4', unit: 'mm' },
            //  pagebreak: { before: '.i-pdf--page' },
        };

        const HEADER_HEIGHT = 0; // mm        
        var elementOffSet = $('.i-pdf-first--page').offset().top; // * 2 + (HEADER_HEIGHT * 3.77) + 30 // px;
        var marginElements = document.getElementsByClassName('.i-pdf--page');

        // Add margins because of scroll height and the header. Will be restored later.
        var j = 0;
        for (var i = 0; i < marginElements.length; i++) {
            if (i == 0) {
                marginElements[i].style.paddingTop = (elementOffSet * (1 * j + 2.1) + (HEADER_HEIGHT * 1)).toString() + 'px';
            }
            else {
                marginElements[i].style.paddingTop = (elementOffSet * (1 * j + 2.3) + (HEADER_HEIGHT * 2)).toString() + 'px';
            }
        }

        var headerElem = document.getElementById('i-Header');

        html2pdf().from(headerElem).set(opt).toImg().get('img').then(function (img) {
            MakeProgress(50, progressLabel);
            html2pdf().from(element).set(opt).toPdf().get('pdf').then(function (pdf) {
                MakeProgress(60, progressLabel);
                var totalPages = pdf.internal.getNumberOfPages();
                //pdf.setFont("helvetica");
                pdf.setFontSize(10);

                
                    for (var i = 1; i <= totalPages; i++) {
                        MakeProgress(60 + i * 10, progressLabel);
                        pdf.setPage(i);

                        if (i != 1) {
                            pdf.addImage(img, 'JPEG', 6, 0, pdf.internal.pageSize.getWidth() - 12, HEADER_HEIGHT);
                        }
                        pdf.text(30, pdf.internal.pageSize.getHeight() - 1, getFooterText(i, totalPages));
                    }
               
            }).get('pdf').then(function (pdf) {
                var emaild = document.getElementById('emailid').value;

               // console.log(emaild);
                if (isSendEmailFlow === 1) {

                    var my64 = '';
                    html2pdf().from(element).outputPdf('blob')
                        .then(pdfBlob => {
                            // Convert PDF blob to Base64
                            const reader = new FileReader();
                            reader.readAsDataURL(pdfBlob);
                            reader.onloadend = function () {
                                const base64data = reader.result;
                                // console.log(btoa(base64data));

                                my64 = base64data;


                            
                                var pdfBase64 = pdf.output();
                               

                                function OnComplete(response) {
                                    for (var i = 0; i < marginElements.length; i++) {
                                        marginElements[i].style.paddingTop = '10px';
                                    }

                                    if (response.responseJSON.d === "1") {
                                        MakeProgress(100, 'Emailed');
                                    }
                                    else {
                                        MakeProgress(100, 'Emailed');
                                        //MakeProgress(99, response.responseJSON.d);
                                        //progressBarElem.classList.add('c-background--red');
                                    }

                                    downloadBtnElem.disabled = false;
                                    downloadBtnElem.classList.remove('btn-disabled');

                                    emailBtnElem.disabled = false;
                                    emailBtnElem.classList.remove('btn-disabled');
                                }
                                //  alert('hi');


                                var emailDatas = {};


                                emailDatas.PDFBase64 = pdfBase64;
                                emailDatas.FileName = fileName;
                                emailDatas.Emailid = emaild;
                                emailDatas.Emailbody = element.innerHTML;

                                $.ajax({
                                    type: "POST",
                                    url: "/Report_Invoice/SendmailStr",
                                    data: "{emailDatas:" + JSON.stringify(emailDatas) + "}",
                                    contentType: "application/json;charset=utf-8",
                                    datatype: "json",
                                    complete: OnComplete
                                });
                            }
                        });
                }
                else if (isSendEmailFlow === 2) {
                    pdf.save(fileName);
                    
                    // Restore Margins added because of scroll height and the header.
                    for (var i = 0; i < marginElements.length; i++) {
                        //marginElements[i].style.paddingTop = '100px';
                    }
                    MakeProgress(100, 'Downloaded');
                    //headerElem.style.backgroundColor = 'white';

                    downloadBtnElem.disabled = false;
                    downloadBtnElem.classList.remove('btn-disabled');

                    if (isEmailIdVerified == 'Y') {
                        emailBtnElem.disabled = false;
                        emailBtnElem.classList.remove('btn-disabled');
                    }
                }
                
            });
        });



   
    }

   

    if (isEmailIdVerified == 'Y') {
        emailBtnElem.disabled = false;
        emailBtnElem.title = 'Email will be sent to ' + emailAddress;
    }
    else {
        emailBtnElem.disabled = true;
        emailBtnElem.title = 'Can not send email as email address ' + emailAddress + ' is not verrified';
    }

    emailBtnElem.classList.remove('displayNone');
    downloadBtnElem.classList.remove('displayNone');
    if (IsSandesActive == 'true') {
        btnSandesElem.classList.remove('displayNone');
    }


    emailBtnElem.addEventListener('click', function () {
        //var headerElem = document.getElementById('i-Header');
        //headerElem.style.backgroundColor = 'red';

        emailBtnElem.disabled = true;
        emailBtnElem.classList.add('btn-disabled');

        downloadBtnElem.disabled = true;
        downloadBtnElem.classList.add('btn-disabled');
        //progressBarElem.classList.add('progress-bar-striped');
        progressBarElem.classList.add('progress-bar-success');
        progressBarElem.classList.remove('displayNone');
        document.getElementById('i-progress').classList.remove('displayNone');
        downloadPdf(1);
    });

    downloadBtnElem.addEventListener('click', function () {
        //var headerElem = document.getElementById('i-Header');
        //headerElem.style.backgroundColor = 'red';
        downloadBtnElem.disabled = true;
        downloadBtnElem.classList.add('btn-disabled');

        emailBtnElem.disabled = true;
        emailBtnElem.classList.add('btn-disabled');

        //progressBarElem.classList.add('progress-bar-striped');
        progressBarElem.classList.add('progress-bar-success');
        progressBarElem.classList.remove('displayNone');
        document.getElementById('i-progress').classList.remove('displayNone');
        downloadPdf(2);
    });

    btnSandesElem.addEventListener('click', function () {
        //debugger;
        btnSandesElem.disabled = true;
        btnSandesElem.classList.add('btn-disabled');
        progressBarElem.classList.add('progress-bar-success');
        progressBarElem.classList.remove('displayNone');
        document.getElementById('i-progress').classList.remove('displayNone');
        downloadPdf(3);

    });

});