package sn.socialnetwork.reports.actions;

import sn.socialnetwork.domain.Entity;
import sn.socialnetwork.reports.Report;
import sn.socialnetwork.reports.ReportType;

public class RemoveReport<ID, E extends Entity<ID>> extends Report<ID, E> {
    public RemoveReport(E target) {
        super(target, ReportType.ADD);
    }

    public String toString() {
        return "Removed " + getTarget().toString();
    }
}
