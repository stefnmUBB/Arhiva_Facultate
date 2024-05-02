package sn.socialnetwork.factory;

import sn.socialnetwork.domain.Entity;
import sn.socialnetwork.reports.AbstractReport;
import sn.socialnetwork.reports.Report;
import sn.socialnetwork.reports.ReportType;
import sn.socialnetwork.reports.actions.AddReport;
import sn.socialnetwork.reports.actions.RemoveReport;
import sn.socialnetwork.reports.actions.UpdateReport;

public class ReportFactory {
    private ReportFactory() {}
    private static final ReportFactory instance = new ReportFactory();
    public static ReportFactory getInstance() { return instance; }

    public <ID,E extends  Entity<ID>> AbstractReport createReport(ReportType type, E entity) {
        switch (type)
        {
            case ADD: return new AddReport<>(entity);
            case UPDATE: return new UpdateReport<>(entity);
            case REMOVE: return new RemoveReport<>(entity);
            default: return new Report<>(entity);
        }
    }
}
