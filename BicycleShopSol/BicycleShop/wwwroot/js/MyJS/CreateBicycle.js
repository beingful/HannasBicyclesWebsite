let price = document.querySelector("input[name=Price]")
let discount = document.querySelector("input[name=Discount]")
let total = document.querySelector("input[name=Total]")

price.addEventListener('change', e => {
    total.value = Number(e.target.value) * (1 - Number(discount.value) / 100.);
})

discount.addEventListener('change', e => {
    total.value = Math.floor(Number(price.value) * (1 - Number(e.target.value) / 100.));
})

let imagePath = document.querySelector("input[name=ImagePath]")
let image = document.getElementById('image')

imagePath.addEventListener('change', e => {
    image.src = e.target.value;
})