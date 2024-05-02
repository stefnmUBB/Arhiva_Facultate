function lab4_1()
  spamW = strsplit(fileread("keywords_spam.txt"),' ');
  spamUW =unique(spamW);
  spamUW = spamUW(2:end) % scapam de ' ' sau '\n'

  spamFrq = [];
  spamL = length(spamW);
  spamUL = length(spamUW);

  for i = 1:spamUL
    counter=0;
    for j = 1:spamL
      counter+=strcmp(spamUW(i), spamW(j));
    endfor
    spamFrq = [spamFrq, counter/spamL];
  endfor

  hamW = strsplit(fileread("keywords_ham.txt"),' ');
  hamUW =unique(hamW);
  hamUW = hamUW(2:end) % scapam de ' ' sau '\n'

  hamFrq = [];
  hamL = length(hamW);
  hamUL = length(hamUW);

  for i = 1:hamUL
    counter=0;
    for j = 1:hamL
      counter+=strcmp(hamUW(i), hamW(j));
    endfor
    hamFrq = [hamFrq, counter/hamL];
  endfor

  spamFrq
  hamFrq

  email1 =strsplit(fileread("email1.txt"),' ');

  spamFrq1 = []
  hamFrq1 = []

  for i=1:spamUL
    counter = 0;
    for j=1:length(email1)
      counter+=strcmp(spamUW(i), email1(j));
    endfor
     if counter==0
      spamFrq1 = [spamFrq1,1-spamFrq(i)];
    else
      spamFrq1 = [spamFrq1, spamFrq(i)];
    endif
  endfor


   for i=1:hamUL
    counter = 0;
    for j=1:length(email1)
      counter+=strcmp(hamUW(i), email1(j));
    endfor
    if counter==0
      hamFrq1 = [hamFrq1,1-hamFrq(i)];
    else
      hamFrq1 = [hamFrq1, hamFrq(i)];
    endif
  endfor

  spam = prod(spamFrq1) * spamL/ (spamL+hamL)
  ham = prod(hamFrq1) * hamL/ (spamL+hamL)

  email2 =strsplit(fileread("email2.txt"),' ');

  spamFrq1 = []
  hamFrq1 = []

  for i=1:spamUL
    counter = 0;
    for j=1:length(email2)
      counter+=strcmp(spamUW(i), email2(j));
    endfor
     if counter==0
      spamFrq1 = [spamFrq1,1-spamFrq(i)];
    else
      spamFrq1 = [spamFrq1, spamFrq(i)];
    endif
  endfor


   for i=1:hamUL
    counter = 0;
    for j=1:length(email2)
      counter+=strcmp(hamUW(i), email2(j));
    endfor
    if counter==0
      hamFrq1 = [hamFrq1,1-hamFrq(i)];
    else
      hamFrq1 = [hamFrq1, hamFrq(i)];
    endif
  endfor

  spam = prod(spamFrq1) * spamL/ (spamL+hamL)
  ham = prod(hamFrq1) * hamL/ (spamL+hamL)


end
