let football = document.getElementById('football')
let ball = document.getElementById('ball')

football.addEventListener('click', function (e) {
    const coords = {
        x: e.x,
        y: e.y
    }
    maxWidth = document.documentElement.clientWidth
    maxLength = document.documentElement.clientHeight
    if(coords.x >= 50 && coords.x <= maxWidth - 50 && coords.y >= 50 && coords.y <= maxLength - 50) {
    ball.style.left = `${coords.x - 50}px`
    ball.style.top = `${coords.y - 50}px`
    console.log(e.x)
    console.log(e.y)
    }
})