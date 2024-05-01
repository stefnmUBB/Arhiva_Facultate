days_in_month = [0,31,28,31,30,31,30,31,31,30,31,30,31]
class DateValidator:
    @staticmethod
    def validate_date(date):
        if date.month<1 or date.month>12:
            raise ValueError("Month must be a number from 1 to 12")
        d_i_m = days_in_month[date.month]
        if date.day<1 or date.day>d_i_m:
            raise ValueError("Invalid day in date")
