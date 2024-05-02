package com.example.pw2.pb3;

import com.example.pw2.Service;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "pb3AdminPage", value = "/pb3/admin")
public class AdminPageServlet extends HttpServlet {
    @Override
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        var adminId = request.getParameter("adminId");
        if(adminId==null){
            throw new RuntimeException("Admin not logged in");
        }
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        out.println("<html><body>");
        out.println("<h1> [Pb3] Admin " +  adminId + " Page</h1>");

        for(var com: Service.getUnapprovedComments()){
            out.println(com.getId());
            out.println("<b>"+com.getName()+"</b>");
            out.println(com.getContent());
            out.println("<form action='approve' method='POST'><input type='text' name='comId' value='"+com.getId()+"' hidden><input type='submit' name='Approve'></form><br/>");
        }

        out.println("</body></html>");
    }
}
