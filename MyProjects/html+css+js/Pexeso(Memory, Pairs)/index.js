const cards = document.querySelectorAll('.memory-card')
let hasFlippedCard = false
let firstCard, secondCard
let isLocked = false

const isMatching = () => {
    if (firstCard.dataset.framework === secondCard.dataset.framework) {
        firstCard.removeEventListener('click', flipCard)
        secondCard.removeEventListener('click', flipCard)
    } else {
        isLocked = true
        setTimeout(() => {
            firstCard.classList.remove('flip')
            secondCard.classList.remove('flip')
            isLocked = false
        }, 2000)
    }
}

const flipCard = e => {
    if (!isLocked) {
        const target = e.target.parentElement
        target.classList.add('flip')
        if (!hasFlippedCard) {
            hasFlippedCard = true
            firstCard = target
        } else {
            hasFlippedCard = false
            secondCard = target
            isMatching()
        }
    }
}

cards.forEach(card => card.addEventListener('click', flipCard))
