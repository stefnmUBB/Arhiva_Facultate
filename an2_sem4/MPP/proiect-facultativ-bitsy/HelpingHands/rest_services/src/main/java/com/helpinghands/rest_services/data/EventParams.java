package com.helpinghands.rest_services.data;

import com.helpinghands.domain.Eveniment;
import com.helpinghands.rest_services.dto.EvenimentDTO;

public class EventParams extends UserActionParams {
    private EvenimentDTO eveniment;


    public EvenimentDTO getEveniment() {
        return eveniment;
    }

    public void setEveniment(EvenimentDTO eveniment) {
        this.eveniment = eveniment;
    }
}
