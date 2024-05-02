package com.helpinghands.repo;

import com.helpinghands.domain.Participant;

public class ParticipantRepo extends AbstractRepo<Participant> implements IParticipantRepo{
    public ParticipantRepo() {
        super(Participant.class);
    }
}
