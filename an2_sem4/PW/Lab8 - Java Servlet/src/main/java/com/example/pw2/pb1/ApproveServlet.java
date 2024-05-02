package com.example.pw2.pb1;

import com.example.pw2.Service;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "pb1Approve", value = "/pb1/approve")
public class ApproveServlet extends HttpServlet {
    @Override
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        var id=Integer.parseInt(request.getParameter("comId"));
        Service.approveComment(id);
        PrintWriter out = response.getWriter();
        out.println("OK");
    }
}
