package com.helpinghands.repo;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.repo.data.EventOrderOption;

public interface IEvenimentRepo extends IRepo<Eveniment> {
    Eveniment[] getOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage);

    Eveniment[] getByInterestOrderedPaged(EventOrderOption orderOption, int page, int itemsPerPage, String interest);

    Eveniment[] getByOrganizer(int organizerId);
    Eveniment[] getByVoluntar(int volId);


}
