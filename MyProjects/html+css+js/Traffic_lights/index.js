const defaultColor = window.getComputedStyle(document.body).backgroundColor
const next = document.getElementById('next')

let counter = 2

next.addEventListener('click', function() {
    counter++
    let prevLight
    let curLight
    if(counter == 3) {
        prevLight = 'yellow'
        curLight = 'red'
        counter = -1
    }else if(counter == 1) {
        prevLight = 'yellow'
        curLight = 'green'
    }else {
        if(counter == 2) {
            prevLight = 'green'
        }else {
            prevLight = 'red'
        }
        curLight = 'yellow'
    }
    document.getElementById(prevLight).style.backgroundColor = defaultColor
    document.getElementById(curLight).style.backgroundColor = curLight
})