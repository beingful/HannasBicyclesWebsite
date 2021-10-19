let text = ['“She looks like such a mess,” they said, turning up their noses at Beauty.  “She might as well serve us.”  And so Beauty did all the hard work.  And then – good news! – the father’s ship came to shore!  “Daughters,” said the happy father, “I am going to town.  Tell me what fine gift I can bring back for you.”“Bring me the finest dress from the finest shop,” said the eldest sister.“I want one just like it,” said the middle sister.“And you, Beauty?” said he.“All I want, Father,” said she, “is a single rose.”',
'“Can you believe her?” said the eldest sister. “What a fool!” said the middle sister.  They both laughed.“Girls!” said the father.  “If that is what Beauty wants, that’s what I will bring back for her.”',
'The father was on his way home when he thought, “I forgot all about the rose for Beauty!”  All at once, the sky turned black.  “Oh, dear!” he said.  “A storm is coming!”',
'A moment later, heavy dark rains fell from the sky.  Soaking wet and tired, the father saw a blink of light from far away.  He went closer to the light, hoping it meant there was some place he could ask to stay the night.  When he got up close, he saw a large palace with candles in all its windows.  It was very odd, but the garden gate was open.  And so with care, the father stepped in.',
'“Hello?” he said.  No answer.  There, before him, was a great feast over a long table.  “Hello?” he said again.  Still, no answer.  The father sat down in front of the fire to warm himself, and he waited.  But still, no one came.  “I suppose it would be all right if I stay the night,” said the father.  He took a quick bite from the feast, found a bedroom, and fell fast asleep.The next morning the table was laid again, but this time with breakfast.  Again - most odd! - no one was around.  “I suppose I should leave,” said the father after a while.  On the way out he passed a rose garden.  “I will take just one,” said he.  And he picked a rose for Beauty.Just then, a loud stomp came up from behind him. Roared a voice - “You took my rose!”']

counter = 0

let cont = document.getElementById('text')
let next = document.getElementById('load')

next.addEventListener('click', function() {
    cont.innerText = text[counter]
    if (counter == text.length - 1) {
        next.style.pointerEvents = 'none'
    }
    counter++
})