import { useState } from 'react'

function FormattedDate({date}){
 return (<span>{date.getDate()}/{date.getMonth()+1}/{date.getFullYear()} {date.getHours()}:{date.getMinutes()}</span>);
}

export default FormattedDate
