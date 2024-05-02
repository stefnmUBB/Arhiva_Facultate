<%@ taglib prefix = "ct" uri = "../WEB-INF/calendar.tld"%>
<html>
    <head>
        <title>Calendar</title>
        <style>
            .cal1 {
                background:wheat;

            }
            .cal2 {
                background:black;
                color:white;
            }
            .cal2 th, .cal2 td {
                padding:5px;
                border:1px solid white;
            }
        </style>
    </head>
    <body>
        <ct:Calendar an="2010" luna="1" zi="8" culoare="#FF0000" className="cal1"/>

        <ct:Calendar an="2023" luna="5" zi="30" culoare="#00FFFF" className="cal2"/>
    </body>
</html>