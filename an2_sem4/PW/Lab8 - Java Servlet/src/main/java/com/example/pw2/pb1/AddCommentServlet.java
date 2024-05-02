package com.example.pw2.pb1;

import com.example.pw2.Service;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "pb1AddComment", value = "/pb1/add_comment")
public class AddCommentServlet extends HttpServlet {

    public void init() {

    }

    @Override
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        var name = request.getParameter("name");
        var content = request.getParameter("content");
        Service.addComment(name, content);

        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        out.println("OK");
    }


    public void destroy() {
    }
}