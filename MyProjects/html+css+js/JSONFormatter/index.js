let run = document.getElementById('formate')
let input = document.getElementById('input')
let output = document.getElementById('output')
let container = document.getElementById('any')
let error

run.addEventListener('click', e => {
    output.value = ''
    let inputText = input.value
    let charsEndl = ['{', '}', ']']
    let charsTab = ['{', '}']
    let coma = 0
    if (error != undefined) {
        container.removeChild(error)
    }
    if (!check(inputText)) {
        error = document.createElement('div')
        error.id = 'error'
        error.className = 'some'
        error.innerText = '[format error]'
        container.appendChild(error)
        return false
    }
    for(var char of inputText) {
        if (charsEndl.includes(char)) {
            if (char == '{' && coma == 1) {
                coma = 0
            } else {
                if (char == '}') {
                    coma++
                }
                output.value += '\n'
                if (char == ',') {
                    output.value += '  '
                }
            }
            if (charsTab.includes(char)) {
                output.value += '  '
            }
        }
        output.value += char
        if (char == ',' || char == '{') {
            output.value += '\n'
            if (char != ',' || coma != 1) {
                output.value += '    '
            }
        }
    }
})

const check = (text) => {
    let opSqBr, opFigBr, clFigBr, clSqBr
    opSqBr = opFigBr = clFigBr = clSqBr = 0
    for(let char of text) {
        if (char == '[') {
            opSqBr++
        }else if (char == '{') {
            opFigBr++
        }else if (char == '}') {
            clFigBr++
        }else if (char == ']') {
            clSqBr++
        }
    }
    if (opSqBr == 0 || opSqBr != clSqBr || clSqBr == 0 ||
        opFigBr == 0 || opFigBr != clFigBr || clFigBr == 0) {
        return false
    }
    return true
}