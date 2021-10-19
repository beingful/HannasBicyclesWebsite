let images = ['https://pngicon.ru/file/uploads/chai-512x369.png',
'https://pngicon.ru/file/uploads/naruto-436x512.png',
'https://pngicon.ru/file/uploads/kupidon-512x474.png',
'https://pngicon.ru/file/uploads/taxi-512x415.png',
'https://pngicon.ru/file/uploads/ulibka-512x358.png',
'https://pngicon.ru/file/uploads/doroga-512x366.png',
'https://pngicon.ru/file/uploads/bokali-478x512.png',
'https://pngicon.ru/file/uploads/tigry-512x495.png',
'https://pngicon.ru/file/uploads/igralnye-karty-484x512.png',
'https://pngicon.ru/file/uploads/znak-stop-497x512.png',
'https://pngicon.ru/file/uploads/ezhik2-512x318.png',
'https://pngicon.ru/file/uploads/belaja-koshka-png-512x449.png',
'https://pngicon.ru/file/uploads/zolotoy-budda-386x512.png',
'https://pngicon.ru/file/uploads/anime-girls-385x512.png',
'https://pngicon.ru/file/uploads/ljagushonok-pepe-512x486.png']

let arrows = document.getElementsByClassName('move')
let container = document.getElementById('container')
let image = document.getElementById('image')

console.log(container)
console.log(arrows)
console.log(image)

let counter = 0;

for(let arrow of arrows) {
    arrow.addEventListener('click', function(e) {
        if(e.target.id == 'next') {
            arrows[1].style.pointerEvents = 'all'
            if(counter >= images.length - 2) {
                e.target.style.pointerEvents = 'none'
            }
            ++counter
            image.src = images[counter]
            container.appendChild(image)
        }else{
            arrows[0].style.pointerEvents = 'all'
            if(counter >= 1) {
                counter--
                image.src = images[counter]
                container.appendChild(image)
            }
            if(counter == 0){
                e.target.style.pointerEvents = 'none'
            }
        }
    })
}