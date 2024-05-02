package com.helpinghands.repo;

import com.helpinghands.domain.CerereSponsor;
import com.helpinghands.domain.SponsorType;
import com.helpinghands.domain.Voluntar;

public interface ICerereSponsorRepo extends IRepo<CerereSponsor>{
    CerereSponsor applyforSponsorship(CerereSponsor cerereSponsor);

}
