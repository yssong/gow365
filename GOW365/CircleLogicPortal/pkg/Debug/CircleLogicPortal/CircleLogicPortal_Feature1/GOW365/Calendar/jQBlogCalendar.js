<script type="text/javascript" src="/_layouts/jQCalendarBlog/jquery-1.8.2.min.js"></script>
<script src="/_layouts/jQCalendarBlog/jquery.ui.core.min.js"></script>
<script src="/_layouts/jQCalendarBlog/jquery.ui.datepicker.min.js"></script>
<link rel="stylesheet" type="text/css" href="/_layouts/jQCalendarBlog/jquery-ui-1.10.1.custom.min.CSS"/>

  <script type="text/javascript">

   JSRequest.EnsureSetup();
   
    var firstDayOfMonth;
    var lastDayOfMonth;
    var formattedFirstDay;
    var formattedLastDay;
    var today = new Date();
    var currDate = new Date();
    var liHtml;
    var calliHtml;
    var calliHtml1;
    var itemURL;
    var date;
    var currentDate =  null;  
    var currWeekDate = null;
    var siteRelUrl = L_Menu_BaseUrl;
    var camlFields;
    var camlQuery;
    var camlOptions;
    var formattedTime;
    var soapEnv;
    var i;
   
   $(document).ready(function() {
    $( ".divDatePicker" ).datepicker({
      onChangeMonthYear: function(year, month, inst) { 
      FillCalendar(year, month);
      }
     });
    
    FillCalendar(today.getFullYear(), today.getMonth()+1);
   });


    function FillCalendar(year, month) {
        firstDayOfMonth = new Date(year, month, 1);
        lastDayOfMonth = new Date(year, month, 0);        


        formattedFirstDay = firstDayOfMonth.getFullYear() + "-" + month + "-" + firstDayOfMonth.getDate();
        formattedLastDay = lastDayOfMonth.getFullYear() + "-" + month + "-" + lastDayOfMonth.getDate();


        camlFields = "<ViewFields><FieldRef Name='Title' /><FieldRef Name='PublishedDate' /></ViewFields>";
        camlQuery = "<Query><Where><And><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>0</Value></Eq><And><Geq><FieldRef Name='PublishedDate' /><Value Type='DateTime'>" + formattedFirstDay + "</Value></Geq><Leq><FieldRef Name='PublishedDate' /><Value Type='DateTime'>" + formattedLastDay + "</Value></Leq></And></And></Where></Query>";


        soapEnv =
          "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'> \
                <soapenv:Body> \
                     <GetListItems xmlns='http://schemas.microsoft.com/sharepoint/soap/'> \
                        <listName>Posts</listName> \
                        <query> \
                          <Query> \
                            <Where> \
                             <And>\
                              <Eq>\
                               <FieldRef Name='_ModerationStatus' /> \
                               <Value Type='ModStat'>0</Value> \
                               </Eq>\
                               <And>\
                               <Geq>\
                               <FieldRef Name='PublishedDate' />\
                               <Value Type='DateTime'>" + formattedFirstDay + "</Value>\
                               </Geq>\
                               <Leq>\
                               <FieldRef Name='PublishedDate' />\
                               <Value Type='DateTime'>" + formattedLastDay + "</Value>\
                               </Leq>\
                               </And>\
                              </And>\
                             </Where>\
                            <OrderBy>\
                             <FieldRef Name='PublishedDate' Ascending='True' />\
                            </OrderBy>\
                            </Query>\
                            </query> \
                      </GetListItems> \
                    </soapenv:Body> \
                </soapenv:Envelope>";   
        
        $.ajax({
        url: siteRelUrl + "/_vti_bin/lists.asmx",
        type: "POST",
        dataType: "xml",
        data: soapEnv,
        complete: processResult,
        contentType: "text/xml; charset=\"utf-8\""
        });

    }



    function processResult(xData, status) {              
        currentDate = null;
        $(xData.responseXML).find("z\\:row").each(function() { 
        
        date = new Date($(this).attr("ows_PublishedDate").substring(0,4), $(this).attr("ows_PublishedDate").substring(5,7)-1, $(this).attr("ows_PublishedDate").substring(8,10), $(this).attr("ows_PublishedDate").substring(11,13), $(this).attr("ows_PublishedDate").substring(14,16), $(this).attr("ows_PublishedDate").substring(17,19));


        itemURL = siteRelUrl + "/Lists/Posts/Post.aspx?ID=" + $(this).attr("ows_ID");

        
        $('.ui-datepicker-calendar a')
        .filter(function(index){
        return  $(this).text() == date.getDate() && 
        $(this).parent('td').attr("data-year") == date.getFullYear() && 
        $(this).parent('td').attr("data-month") == date.getMonth();
        }).css("border", "2px solid #2989d1");		
        
        
        if(currentDate == null || date.getDate() != currentDate.getDate()){ 
            CreatePopUp(date, itemURL, $(this).attr("ows_Title"));
        }

        else if(date.getDate() == currentDate.getDate()){
        
            calliHtml = '<li class="divCalendarLI"><a class="anchCalLi" href="' + itemURL + '">' + $(this).attr("ows_Title") + '</a></li>';
            $("#" + date.getDate() + "_eventPopUp" + " .divCalendarUL").append(calliHtml);
        }    
        
        currentDate = date; 

        }); 
    }   



 function CreatePopUp(eventDate, itemURL, title){

    
  	$('.ui-datepicker-calendar a')
       .filter(function(index){
            return  $(this).text() == eventDate.getDate() && 
                    $(this).parent('td').attr("data-year") == eventDate.getFullYear() && 
                    $(this).parent('td').attr("data-month") == eventDate.getMonth();
       }).parent('td').append("<div class='eventPopUpDiv' id='" + eventDate.getDate() + "_eventPopUp' style='display:none'></div> ");
          
    $('.ui-datepicker-calendar a')
       .filter(function(index){
            return  $(this).text() == eventDate.getDate() && 
                    $(this).parent('td').attr("data-year") == eventDate.getFullYear() && 
                    $(this).parent('td').attr("data-month") == eventDate.getMonth();
            }).parent('td').mouseover(function(){
            document.getElementById($(this).find('a').first().text() + "_eventPopUp").style.display = "inline";
       });
           
      $('.ui-datepicker-calendar a')
       .filter(function(index){
            return  $(this).text() == eventDate.getDate() && 
                    $(this).parent('td').attr("data-year") == eventDate.getFullYear() && 
                    $(this).parent('td').attr("data-month") == eventDate.getMonth();
            }).parent('td').mouseout(function(){
            document.getElementById($(this).find('a').first().text() + "_eventPopUp").style.display = "none";
      });           
      	     
      calliHtml = '<li class="divCalendarLI"><a class="anchCalLi" href="' + itemURL + '">' + title + '</a></li>';
       
       $("#" + eventDate.getDate() + "_eventPopUp").append("<ul class = 'divCalendarUL'>");
       $("#" + eventDate.getDate() + "_eventPopUp" + " .divCalendarUL").append(calliHtml); 
       
             
  }
     
</script>
   

<div class="divDatePicker"></div>    


   
<style type="text/css">

.eventPopUpDiv {
	Z-INDEX: 9002; PADDING-RIGHT: 10px; BORDER-BOTTOM: black 1px solid; POSITION: absolute; BORDER-LEFT: black 1px solid; BACKGROUND-COLOR: white; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid
}
.CalendarLI {
	MARGIN-LEFT: -23px; FONT-WEIGHT: normal
}
.divCalendarLI {
	MARGIN-LEFT: -23px
}
.calHead {
	PADDING-LEFT: 4px; PADDING-RIGHT: 4px; FONT-SIZE: 8pt !important; FONT-WEIGHT: bold !important
}
.anchCalLi {
	TEXT-ALIGN: left !important;
}
.anchCalLi:hover {
	BORDER-RIGHT-WIDTH: 0px; BACKGROUND: none transparent scroll repeat 0% 0%; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; FONT-WEIGHT: normal !important
}
.anchCalLi:visited {
    text-decoration: none; color: rgb(0, 114, 188) !important;
}
.ui-state-default:visited {
    text-decoration: none; color: rgb(0, 114, 188) !important;
}

</style>
