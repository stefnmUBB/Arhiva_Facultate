def print_table(lines: list[list[any]], headers: list[str] = None, title: str=""):
    result=""
    if headers is None:
        headers = []
    lines: list[list[str]] = list(map(lambda x: list(map(str, x)), lines))

    cols_count = len(max(lines,key=len))
    cols_count = max(cols_count, len(headers))

    while len(headers)<cols_count:
        headers.append(" ")

    col_span = list(map(len, headers))
    for i in range(len(lines)):
        for j in range(len(lines[i])):
            col_span[j] = max(col_span[j], len(lines[i][j]))

    r_len = sum(col_span) + len(col_span * 3)-2

    if title is not None and len(title)>0:
        if len(title)>r_len:
            col_span[-1] += len(title)-r_len
            r_len = sum(col_span) + len(col_span * 3)-2

        result+="|"
        result+=title.center(r_len)
        result+="|"+"\n"


    result+="|"
    for i in range(len(headers)):
        result+=headers[i].ljust(col_span[i])+" | "
    for i in range(len(headers), cols_count):
        result+="-".ljust(col_span[i]) + " | "
    result+="\n"


    result+="|"
    result+="-"*r_len
    result+="|"+"\n"


    for line in lines:
        result+="|"
        for i in range(len(line)):
            result+=line[i].ljust(col_span[i])+" | "
        for i in range(len(line), cols_count):
            result+="-".ljust(col_span[i]) + " | "
        result+="\n"
    return result