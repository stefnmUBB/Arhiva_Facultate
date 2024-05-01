from service.list_search_engine_validator import ListSearchEngineValidator


class ListSearchEngine:
    @staticmethod
    def look_for(text,list):
        """
        Searches a text in a list of Searchable elements.
        An element will be chosen if it contains at least one key specified in search text
        :param text: text to be searched
        :param list: list of Searchables
        :return: list of elements in the given list which meet the search requirements
        """
        ListSearchEngineValidator.validate_list(list)
        results = []
        words = text.split()
        if len(words)==0:
            return list
        for element in list:
            for word in words:
                if element.search_in_object(word):
                    results.append(element)
                    break
        return results

    @staticmethod
    def look_for_strict(text, list):
        """
        Searches a text in a list of Searchable elements
        An element will be chosen if it contains all keys specified in search text
        :param text: text to be searched
        :param list: list of Searchables
        :return: list of elements in the given list which meet the search requirements
        """
        ListSearchEngineValidator.validate_list(list)
        results = []
        words = text.split()
        if len(words)==0:
            return list
        for element in list:
            add_it = True
            for word in words:
                if not element.search_in_object(word):
                    add_it = False
                    break
            if add_it:
                results.append(element)
        return results
