package com.example.pw2.pb1;

import com.example.pw2.Service;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "pb1Comments", value = "/pb1/public_page")
public class PublicPageServlet extends HttpServlet {
    @Override
    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        out.println("<body><h2>Articol furat de pe wikipedia :)</h2><p>Continutul articolului <a href=\"#\">aici</a>frkmmfrkelmrke lmflre mflkremgltenmg ln\n" +
                "        frkmmfrkelmrke lmflre mflkremgltenmg ln\n" +
                "        frkmmfrkelmrke lmflre mflkremgltenmg ln\n" +
                "        frkmmfrkelmrke lmflre mflkremgltenmg ln\n" +
                "    </p>\n" +
                "    <h2>Comentarii</h2>\n");
        out.println("<form action='add_comment' method='POST'>" +
                "Nume : <input type='text' name='name'><br/>" +
                "Text : <input type='text' name='content'><br/>" +
                "<input type='submit'>" +
                "</form>");


        for(var com: Service.getApprovedComments()){
            out.println("<b>"+com.getName()+"</b>");
            out.println(com.getContent());
            out.println("<br/>");
        }

        out.println("</body>");
    }


    public void destroy() {
    }
}
