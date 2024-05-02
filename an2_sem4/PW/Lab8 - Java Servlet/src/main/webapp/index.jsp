<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <title>JSP - Hello World</title>
</head>
<body>
    <h1><%= "Servlet" %></h1>

    Server info: <%= application.getServerInfo() %><br>
    Servlet version: <%= application.getMajorVersion() %>.<%= application.getMinorVersion() %><br>
    JSP version: <%= JspFactory.getDefaultFactory().getEngineInfo().getSpecificationVersion() %><br>
    Java version: <%= System.getProperty("java.version") %><br>

    <ul>
        <li>Pb1 :
            <a href=" ./pb1/login2.jsp">admin</a>
            <a href=" ./pb1/public_page">public</a>
        </li>
        <li><a href="./pb2">pb2</a></li>
        <li>Pb3 :
            <a href=" ./pb3/login2.jsp">admin</a>
            <a href=" ./pb3/public_page">public</a>
        </li>
        <li><a href="./pb4">pb4</a></li>
        <li><a href="./pb5">pb5</a></li>
        <li><a href="./pb6">pb6</a></li>
    </ul>
</body>
</html>