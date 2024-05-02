package sn.socialnetwork.reports;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/**
 * a report containing multiple sn.socialnetwork.reports
 */
public class CombinedReport extends AbstractReport {
    private final List<AbstractReport> reports = new ArrayList<>();

    public CombinedReport() {
        super(ReportType.OTHER);
    }

    public void add(AbstractReport report) {
        reports.add(report);
    }

    @Override
    public String toString() {
        return reports.stream()
            .map(AbstractReport::toString)
            .collect(Collectors.joining("\n"));
    }
}
