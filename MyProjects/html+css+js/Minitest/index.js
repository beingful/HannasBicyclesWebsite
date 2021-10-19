let newQuestion = '2)How many letters are there in the word "World"?'
let rightAnswer = 5
let newVariants = ['4', '5']
let result = 0
let id = 'next'
let answers = []
let right = 0

let objects = document.getElementById('next')
let question = document.getElementById('question')
let answer1 = document.getElementById('answer1')
let answer2 = document.getElementById('answer2')
let label1 = document.getElementById('l1')
let label2 = document.getElementById('l2')
let gen = document.getElementById('gen')

objects.addEventListener('click', function(e) {
    let choice
    let lab
    if (answer1.checked) {
        choice = answer1
        lab = label1.innerText
    }else if(answer2.checked) {
        choice = answer2
        lab = label2.innerText
    }
    e.preventDefault()
    if(lab == rightAnswer) {
        right++
    }
    if(e.target.innerText == 'Finish') {
        let result = document.createElement('div')
        result.className = 'result'
        result.innerText = `Result: ${right}/2`
        gen.appendChild(result)
    } else if (e.target.innerText == 'Next') {
        question.innerText = newQuestion
        label1.innerText = newVariants[0]
        label2.innerText = newVariants[1]
        e.target.innerText = 'Finish'
    }
    choice.checked = false
})



