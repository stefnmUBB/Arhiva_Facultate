package com.example.pw2.pb4;

import jakarta.servlet.jsp.JspException;
import jakarta.servlet.jsp.JspWriter;
import jakarta.servlet.jsp.tagext.SimpleTagSupport;
import java.io.IOException;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.TextStyle;
import java.util.Calendar;
import java.util.Locale;


public class CalendarTag extends SimpleTagSupport {
    private int an;
    private int luna;
    private int zi=1;
    private String culoare;
    private String className;

    public void setAn(int an) {
        this.an = an;
    }

    public void setLuna(int luna) {
        this.luna = luna;
    }

    public void setZi(int zi) {
        this.zi = zi;
    }

    public void setCuloare(String culoare) {
        this.culoare = culoare;
    }

    public void setClassName(String className) {
        this.className = className;
    }
    public void doTag() throws JspException, IOException {
        var out = getJspContext().getOut();
        var date = LocalDate.of(an, luna, zi);
        var w = date.getDayOfWeek().getValue()-1;
        var mat = new int[6][7];

        for(int i=1;i<=date.getMonth().length(date.isLeapYear());i++){
            mat[w/7][w%7] = date.getDayOfMonth()==i?-i:i;
            w++;
        }


        out.println("<table class='"+className+"'>");
        out.println("<tr><th colspan='7'>"+date.getMonth().getDisplayName(TextStyle.SHORT, Locale.ENGLISH)+" "+date.getYear()+"</th></tr>");
        out.println("<tr><th>L</th><th>M</th><th>M</th><th>J</th><th>V</th><th>S</th><th>D</th></tr>");

        for(int i=0;i<6;i++){
            out.println("<tr>");

            for(int j=0;j<7;j++){
                if(mat[i][j]>0)
                    out.println("<td>"+mat[i][j]+"</td>");
                else if(mat[i][j]==0)
                    out.println("<td></td>");
                else
                    out.println("<td><b style='color:"+culoare+"'>"+(-mat[i][j])+"</b></td>");
            }

            out.println("</tr>");
        }

        out.println("</table>");
    }
}
