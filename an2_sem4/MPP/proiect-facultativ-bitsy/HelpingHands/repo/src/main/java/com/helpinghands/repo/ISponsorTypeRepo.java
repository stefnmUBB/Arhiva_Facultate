package com.helpinghands.repo;

import com.helpinghands.domain.SponsorType;

public interface ISponsorTypeRepo extends IRepo<SponsorType> {
    SponsorType getByName(String name);
}
