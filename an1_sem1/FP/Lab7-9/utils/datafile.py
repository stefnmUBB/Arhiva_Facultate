from domain.client import Client
from domain.movie import Movie
from domain.rent import Rent

class DataFile:
    @staticmethod
    def read_elements(file):
        """
        Reads class objects from file
        The function reads objects serializations following the patters:

        @ObjectType
        Attribute_1 = Value_1
        Attribute_2 = Value_2
        ...
        Attribute_n = Value_n

        creates the objects o = ObjectType(), o.Attribute_i = value_i, i=1,n

        :param file: file to read from
        :return: list of objects
        :raises: Exception if file does not correspond to the expected format
                 or if the object type is not recognised
        """
        result = []
        current_element = None
        lines = file.readlines()
        for line in lines:
            if len(line)==0:
                continue
            if line[0]=='@':
                if current_element is not None:
                    result.append(current_element)
                    current_element = None
                classname=line[1:-1]
                try:
                    current_element = eval(f"{classname}()")
                except:
                    raise Exception(f"Element type {classname} not found")
            else:
                tokens = line[0:-1].split()
                if(len(tokens)>0):
                    if len(tokens)>=3 and tokens[1] == '=':
                        try:
                            try:
                                func=getattr(current_element,"set_"+tokens[0])
                            except:
                                func=getattr(current_element,"_set_"+tokens[0])
                            func(" ".join(tokens[2:]))
                        except Exception as e:
                            print(e)
                            raise Exception("File wrong format")
                    else:
                        raise Exception("File wrong format")
        if current_element is not None:
            result.append(current_element)
        return result

    @staticmethod
    def write_elements(file,list):
        """
        Writes list of object to a file
        :param file: File to save data to
        :param list: list of objects to save
        """
        for element in list:
            file.write("@"+type(element).__name__+"\n")
            for field in element.__annotations__:
                try:
                    func = getattr(element, "get_" + field + "_str")
                except:
                    func = getattr(element, "get_" + field)
                value = func() #getattr(element,field)
                if value is not None and str(value)!="":
                    file.write(f"{field} = {value}\n")
            file.write('\n')


