function out = aranj(v, k)
  out = [];
  seqs = nchoosek(v,k);
  [rows, cols] = size(seqs);
  for i=1:rows
    row = seqs(i,:)

    out = [out; perms(row)];
  endfor
end

