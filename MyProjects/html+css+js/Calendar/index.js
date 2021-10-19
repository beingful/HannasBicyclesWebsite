  let month = document.getElementById('inmonth')
  let year = document.getElementById('inyear')
  let calender = document.getElementById('look-calendar')
  let generate = document.getElementById('generate')


generate.addEventListener('click', function() {
    let date = 1
    let page = new Date(year.value, month.value - 1, date)
    let curMonth = page.getMonth()
    let day = page.getDay() - 1
        if (day == -1) {
            day = 6
        }
    calendar.innerHTML += '<tr>'
    counter = 1
    calendar.innerHTML += `<tr id="cl${counter}">`
    for(let i = 0; i < day; i++) {
        document.getElementById(`cl${counter}`).innerHTML += '<td></td>'
    } 
    while(curMonth == page.getMonth()) {
        if (day % 7 == 0 && date != 1) {
            calendar.innerHTML += `<tr id="cl${counter}">`
            console.log('*')
        }
        document.getElementById(`cl${counter}`).innerHTML += `<td>${date}</td>`
        if(day % 7 == 6) {
            calendar.innerHTML += '</tr>'
            counter++
            day = 0
        }else {
            day++
        }
        date++
        console.log(date)
        page = new Date(year.value, month.value - 1, date)
    }
})
