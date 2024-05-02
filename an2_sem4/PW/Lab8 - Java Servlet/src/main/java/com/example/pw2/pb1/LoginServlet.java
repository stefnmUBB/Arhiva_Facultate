package com.example.pw2.pb1;

import java.io.*;

import com.example.pw2.Database;
import com.example.pw2.Service;
import com.example.pw2.domain.User;
import com.example.pw2.utils.Hash;
import jakarta.servlet.http.*;
import jakarta.servlet.annotation.*;

import javax.xml.crypto.Data;

@WebServlet(name = "pb1Login", value = "/pb1/login")
public class LoginServlet extends HttpServlet {
    private String message;

    public void init() {
        message = "Login";
    }

    User user;

    @Override
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        var username=request.getParameter("username");
        var password=request.getParameter("password");
        user = Service.login(username, password);

        if(user==null){
            throw new RuntimeException("User not found");
        }

        sendPOSTRedirect(request, response);
    }

    private void sendPOSTRedirect(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");
        String postURL = "admin";
        String value1 = ""+user.getId();
        String content = "<html><body onload='document.forms[0].submit()'><form action=\"" + postURL + "\" method=\"POST\">"
                + "<INPUT TYPE=\"hidden\" NAME=\"adminId\" VALUE=\"" + value1 + "\"/>"
                + "</form></body></html>";

        response.setStatus(HttpServletResponse.SC_OK);
        PrintWriter out = response.getWriter();
        out.write(content);
    }

    @Override
    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        out.println("<html><body>");
        out.println("<h1>" + message + "</h1>");
        out.println("</body></html>");
    }


    public void destroy() {
    }
}