package sn.socialnetwork.reports;

public class AbstractReport {
    ReportType type;

    public AbstractReport(ReportType type) {
        this.type = type;
    }

    public String toString() {
        return "This is a report.";
    }
}
