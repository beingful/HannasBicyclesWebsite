class Radio {
    constructor(changes, text) {
        this.changes = changes
        this.text = text
    }
}

Radio.prototype.setAlign = function() {
    let align = Array.from(this.changes).find(elem => elem.checked == true)
    if(align != undefined) {
        this.text.style.textAlign = align.value
    }
}

class CheckBox {
    constructor(changes, text) {
        this.changes = changes
        this.text = text
    }
}

CheckBox.prototype.setEdits = function() {
    let checked = Array.from(this.changes).filter(elem => elem.checked == true)
    for(let edit of checked) {
        if(edit.value == 'bold') {
            this.text.style.fontWeight = edit.value
        }else if(edit.value == 'underline') {
            this.text.style.textDecoration = edit.value
        }else if(edit.value == 'italic') {
            this.text.style.fontStyle = edit.value
        }
    }
}

CheckBox.prototype.deleteEdits = function() {
    this.text.style.fontWeight = 'normal'
    this.text.style.textDecoration = 'none'
    this.text.style.fontStyle = 'normal'
}

let align = document.getElementsByClassName('rb')
let edits = document.getElementsByClassName('cb')
let text = document.getElementById('text')
let show = document.getElementById('show')

show.addEventListener('click', function() {
    let alignEdit = new Radio(align, text)
    let textEdit = new CheckBox(edits, text)
    textEdit.deleteEdits()
    alignEdit.setAlign()
    textEdit.setEdits()
})