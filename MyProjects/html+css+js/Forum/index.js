let texts = document.getElementsByClassName('field')
let addComment = document.getElementById('addNew')
let send = document.getElementById('send')
let container = document.getElementById('container')

function creation(tag, elemClass, text='') {
    let newElem = document.createElement(tag)
    newElem.className = elemClass
    if(text != '') {
        newElem.innerText = text
    }
    return newElem
}

function addZero(value) {
    if(value < 10) {
        return '0'+ value
    }
    return value
}

function setDate(date) {
    return addZero(date.getHours()) + ':' +
            addZero(date.getMinutes()) + ':' +
            addZero(date.getSeconds()) + ' ' +
            addZero(date.getDate()) + '.' +
            addZero(date.getMonth() + 1) + '.'+
            date.getFullYear()
}

console.log()
send.addEventListener('click', function () {
    let userName = Array.from(texts).find(el => el.id == 'name').value
    let message = Array.from(texts).find(el => el.id == 'remark').value
    let comment = creation('div', 'comment')
    let newData = creation('div', 'data')
    let newName = creation('div', 'name', userName)
    newData.appendChild(newName)
    let date = setDate(new Date())
    let newDate = creation('div', 'date', date)
    newData.appendChild(newDate)
    comment.appendChild(newData)
    let newMessage = creation('div', 'text', message)
    comment.appendChild(newMessage)
    container.insertBefore(comment, addComment)
    container.style.height = container.clientHeight + comment.clientHeight + 
        + parseFloat(window.getComputedStyle(comment).marginTop.replace('px', '')) + 'px'
    console.log(container.clientHeight)
    console.log(comment.clientHeight)
    Array.from(texts).forEach(el => el.value = '')
})

