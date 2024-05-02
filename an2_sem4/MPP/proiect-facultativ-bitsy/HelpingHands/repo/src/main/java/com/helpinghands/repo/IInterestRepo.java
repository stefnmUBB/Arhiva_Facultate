package com.helpinghands.repo;

import com.helpinghands.domain.Interest;

public interface IInterestRepo extends IRepo<Interest>{
    Interest getByName(String name);
}
