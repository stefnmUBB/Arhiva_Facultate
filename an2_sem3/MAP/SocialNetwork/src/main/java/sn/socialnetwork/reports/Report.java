package sn.socialnetwork.reports;

import sn.socialnetwork.domain.Entity;

public class Report<ID, E extends Entity<ID>> extends AbstractReport {
    E target;

    public Report(E target) {
        super(ReportType.OTHER);
        this.target = target;
    }

    public E getTarget() {
        return target;
    }

    public Report(E target, ReportType type) {
        super(type);
        this.target = target;
    }

    public String toString() {return "Action performed with " + target;}
}
