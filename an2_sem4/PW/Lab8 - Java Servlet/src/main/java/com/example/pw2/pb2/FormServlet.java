package com.example.pw2.pb2;

import com.example.pw2.Service;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "pb2Form", value = "/pb2")
public class FormServlet extends HttpServlet {
    private static String[] capTitle=new String[]{"Programatori", "Dulciuri"};
    private static int[][] capVals=new int[][]{
            new int[]{1,1,1,1,1,1,1,1,1},
            new int[]{1,1,0,1,1,1,0,1,1}
    };

    @Override
    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();

        var imgId = (int)Math.floor(Math.random()*2);
        var img = "./pb2/captcha-"+imgId+".jpg";

        var capForm = "Selecteaza imagini continand:" +
                "<h2>"+capTitle[imgId] + "</h2>"+
                "<div class='cap cap0'></div>" +
                "<div class='cap cap1'></div>" +
                "<div class='cap cap2'></div><br/>" +
                "<div class='cap cap3'></div>" +
                "<div class='cap cap4'></div>" +
                "<div class='cap cap5'></div><br/>" +
                "<div class='cap cap6'></div>" +
                "<div class='cap cap7'></div>" +
                "<div class='cap cap8'></div><br/>" +
                "<input type='submit' name='Check' onclick='checkCaptcha()'>";

        out.println("<head>");
        out.println("<style>");
        out.println(".cap { width:100px; height:100px; display:inline-block; margin:2px; border:5px solid white;}");
        out.println(".cap[sel=\"1\"] { border:5px solid red;}");
        out.println(".cap0 { background:url('"+img+"') 0 0;}");
        out.println(".cap1 { background:url('"+img+"') -100px 0;}");
        out.println(".cap2 { background:url('"+img+"') -200px 0;}");

        out.println(".cap3 { background:url('"+img+"') 0 -100px;}");
        out.println(".cap4 { background:url('"+img+"') -100px -100px;}");
        out.println(".cap5 { background:url('"+img+"') -200px -100px;}");

        out.println(".cap6 { background:url('"+img+"') 0 -200px;}");
        out.println(".cap7 { background:url('"+img+"') -100px -200px;}");
        out.println(".cap8 { background:url('"+img+"') -200px -200px;}");

        out.println("</style>");
        out.println("</head>");
        out.println("<body>");
        out.println("<h2>A form xaxa</h2>");
        out.println("<form action='./pb1/public_page' method='GET' onsubmit='return beforeSubmit(this)'>" +
                "Nume : <input type='text' name='name'><br/>" +
                "Text : <input type='text' name='content'><br/>" +
                "<input type='submit'><br/>" +
                "<div id='capForm' hidden>"+capForm+"</div>"+
                "</form>");
        out.println("</body>");

        out.println("<script>");
        out.println("var captchaOut=[");

        out.println(capVals[imgId][0]);
        for(int i=1;i<9;i++){
            out.print(", "+capVals[imgId][i]);
        }
        out.println("];\n");


        out.println("var captchaSolved=false;\n");
        out.println("function beforeSubmit(){ document.getElementById('capForm').removeAttribute('hidden'); return captchaSolved; }\n");
        out.println("function checkCaptcha(){ var divs = document.querySelectorAll(\"div.cap\");\n" +
                "for(var i=0;i<9;i++){\n" +
                "    var div=divs[i];\n" +
                "    var sel = div.getAttribute(\"sel\");\n" +
                "    if(sel!=captchaOut[i] && (sel==null && captchaOut[i]!=0)) return;\n" +
                "}" +
                "captchaSolved=true; }\n");
        out.println("var divs = document.querySelectorAll(\"div.cap\");\n" +
                "for(var i=0;i<9;i++){\n" +
                "    var div=divs[i];\n" +
                "    div.onclick=function(e){\n" +
                "        var d=e.target;\n" +
                "        if(d.hasAttribute(\"sel\")){\n" +
                "            d.removeAttribute(\"sel\");\n" +
                "        }\n" +
                "        else d.setAttribute(\"sel\",\"1\");\n" +
                "    }\n" +
                "}");
        out.println("</script>");
    }
}
