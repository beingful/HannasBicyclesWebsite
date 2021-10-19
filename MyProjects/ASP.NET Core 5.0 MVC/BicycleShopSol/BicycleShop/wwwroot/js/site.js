function addQueryStringParameter(uri, key, value) {
    var separator = uri.indexOf('?') !== -1 ? "&" : "?"
    return `${separator}${key}=${value}`
}

function updateQueryStringParameter(uri, key, value) {
    let keys = uri.split('&').map(el => el.includes(key) ? Object.assign(el + `,${value}`) : el)
    return keys.join('&')
}

function deleteFromQueryStringParameter(uri, key, value) {
    let keys = uri.split('&')

    for (let elem of keys) {
        if (elem.includes(key)) {
            if (elem.includes(',')) {
                let newKey
                let values = elem.split(',')
                let ind = values.findIndex(val => val == value)

                if (ind == -1) {
                    values.splice(0, 1, values[0].replace(value, ''))
                    newKey = values.join(',').replace(',', '')
                }
                else {
                    values.splice(ind, 1)
                    newKey = values.join(',')
                }

                keys.splice(keys.indexOf(elem), 1, newKey)
            }
            else if (keys.length > 1) {
                if (keys.indexOf(elem) > 0) {
                    keys.splice(keys.findIndex(el => el == elem), 1)
                }
                else {
                    keys.splice(0, 1)
                    return '/?' + keys.join('&')
                }
            }
            else {
                return "/"
            }

            break
        }
    }

    return keys.join('&')
}

function removeFromQueryStringParameter(uri) {
    let keys = uri.split('&')

    keys.splice(0, 1);

    return '/?' + keys.join('&')
}

function replaceQueryStringParameter(uri, key, value) {
    let keys = uri.split('&').map(el => {
        if (el.includes(key)) {
            return el.substr(0, el.indexOf('=') + 1) + value
        }
        return el
    })

    return keys.join('&')
}

const indexHref = window.location.href
let href = window.location.href

$('.sort-ch-bx').each(function (ind, el) {
    el.addEventListener('change', e => {
        let key = $(`#${e.target.id}`).attr('data-type')
        let value = e.target.id

        if (e.target.checked) {
            if (href.includes(key)) {
                href = updateQueryStringParameter(href, key, value);
            }
            else {
                href += addQueryStringParameter(href, key, value);
            }
        }
        
        else {
            href = deleteFromQueryStringParameter(href, key, value)
        }

        if (href.includes($('.page').attr('data-type'))) {
            href = replaceQueryStringParameter(href, $('.page').attr('data-type'), '1');
        }

        if (href.includes($('#search-btn').attr('data-type'))) {
            href = removeFromQueryStringParameter(href);
        }

        window.location.href = href
    })
})

$('#price').on('click', e => {
    let key = e.target.id
    let priceFrom = $('#price-from').val()
    let priceTo = $('#price-to').val()
    let value = priceFrom + '-' + priceTo

    if (!href.includes(e.target.id)) {
        href += addQueryStringParameter(href, key, value);
    }
    else {
        href = replaceQueryStringParameter(href, key, value);
    }

    if (href.includes($('.page').attr('data-type'))) {
        href = replaceQueryStringParameter(href, $('.page').attr('data-type'), '1');
    }

    if (href.includes($('#search-btn').attr('data-type'))) {
        href = removeFromQueryStringParameter(href);
    }

    window.location.href = href
})

$('.sorting').on('change', e => {
    let key = e.target.id
    let value = e.target.value

    if (!href.includes(key)) {
        href += addQueryStringParameter(href, key, value);
    }
    else {
        href = replaceQueryStringParameter(href, key, value);
    }

    window.location.href = href
})

$('.page-btn').on('click', e => {
    let key = $(`#${e.target.id}`).attr('data-type')
    let value = e.target.value

    if (!href.includes(key)) {
        href += addQueryStringParameter(href, key, value);
    }
    else {
        href = replaceQueryStringParameter(href, key, value);
    }

    window.location.href = href
})

$('.card-body').each(function (ind, el) {
    const maxQuantity = el.querySelector('input[name=maxQuantity]').value
    let quantityInputs = el.querySelectorAll('input[name=quantity]')
    let minusBttn = el.querySelector('.minus')
    let plusBttn = el.querySelector('.plus')

    if (maxQuantity == 0) {
        let submits = el.querySelectorAll('#input[type = submit]')

        for (let submit of submits) {
            submit.disabled = true
        }

        minusBttn.disabled = true
        plusBttn.disabled == true

        for (let input of quantityInputs) {
            input.value = 0
        }
    }

    else if (maxQuantity == 1) {
        plusBttn.disabled == true
    }

    else {
        minusBttn.addEventListener('click', e => {
            for (let input of quantityInputs) {
                input.value = Number(input.value) - 1
            }

            if (quantityInputs[0].value == 1) {
                e.target.disabled = true
            }

            if (plusBttn.disabled == true) {
                plusBttn.disabled = false
            }
        })

        plusBttn.addEventListener('click', function (e) {
            for (let input of quantityInputs) {
                input.value = Number(input.value) + 1
            }

            if (quantityInputs[0].value == maxQuantity) {
                e.target.disabled = true
            }

            if (minusBttn.disabled == true) {
                minusBttn.disabled = false
            }
        })
    }
});

$('#search-btn').on('click', e => {
    let key = $(`#${e.target.id}`).attr('data-type');
    let value = $('input[type=search]').val()

    window.location.href = `/?${key}=${value}`
})

let thumbsize = 14;

let slider = document.querySelector('.min-max-slider');
let min = document.querySelector('input.min');
let max = document.querySelector('input.max');
let lower = document.querySelector('span.value.lower');
let upper = document.querySelector('span.value.upper');
let priceFloor = document.querySelector('#price-from');
let priceCeil = document.querySelector('#price-to')

init();

function draw(splitvalue) {
    let legend = document.querySelector('.legend');
    let thumbsize = parseInt(slider.getAttribute('data-thumbsize'));
    let rangewidth = parseInt(slider.getAttribute('data-rangewidth'));
    let rangemin = parseInt(slider.getAttribute('data-rangemin'));
    let rangemax = parseInt(slider.getAttribute('data-rangemax'));

    min.setAttribute('max', splitvalue);
    max.setAttribute('min', splitvalue);

    min.style.width = parseInt(thumbsize + ((splitvalue - rangemin) / (rangemax - rangemin)) * (rangewidth - (2 * thumbsize))) + 'px';
    max.style.width = parseInt(thumbsize + ((rangemax - splitvalue) / (rangemax - rangemin)) * (rangewidth - (2 * thumbsize))) + 'px';
    min.style.left = '0px';
    max.style.left = parseInt(min.style.width) + 'px';
    min.style.top = lower.offsetHeight + 'px';
    max.style.top = lower.offsetHeight + 'px';
    legend.style.marginTop = min.offsetHeight + 'px';
    slider.style.height = (lower.offsetHeight + min.offsetHeight + legend.offsetHeight) + 'px';

    if (max.value > (rangemax - 1)) max.setAttribute('data-value', rangemax);

    max.value = max.getAttribute('data-value');
    min.value = min.getAttribute('data-value');
    lower.innerHTML = min.getAttribute('data-value');
    upper.innerHTML = max.getAttribute('data-value');
}

function init() {
    let rangemin = Number(min.getAttribute('min'));
    let rangemax = Number(max.getAttribute('max'));
    let avgvalue = (rangemin + rangemax) / 2;
    let legendnum = slider.getAttribute('data-legendnum');

    min.setAttribute('data-value', rangemin);
    max.setAttribute('data-value', rangemax);

    slider.setAttribute('data-rangemin', rangemin);
    slider.setAttribute('data-rangemax', rangemax);
    slider.setAttribute('data-thumbsize', thumbsize);
    slider.setAttribute('data-rangewidth', slider.offsetWidth);

    lower.appendChild(document.createTextNode(rangemin));
    slider.insertBefore(lower, min.previousElementSibling);

    upper.appendChild(document.createTextNode(rangemax));
    slider.insertBefore(upper, min.previousElementSibling);

    let legend = document.createElement('div');
    legend.classList.add('legend');

    let legendvalues = [];

    for (let i = 0; i < legendnum; i++) {
        legendvalues[i] = document.createElement('div');
        let val = Math.round(rangemin + (i / (legendnum - 1)) * (rangemax - rangemin));
        legendvalues[i].appendChild(document.createTextNode(val));
        legend.appendChild(legendvalues[i]);
    }

    slider.appendChild(legend);

    draw(avgvalue);

    min.addEventListener("input", e => {
        let minvalue = Math.floor(e.target.value);
        let maxvalue = Math.floor(max.value);

        e.target.setAttribute('data-value', minvalue);
        let avgvalue = (minvalue + maxvalue) / 2;

        draw(avgvalue);

        priceFloor.value = lower.innerHTML;
        priceCeil.value = upper.innerHTML;
    });

    max.addEventListener("input", e => {
        let maxvalue = Math.floor(e.target.value);
        let minvalue = Math.floor(min.value);

        e.target.setAttribute('data-value', maxvalue);
        let avgvalue = (minvalue + maxvalue) / 2;

        draw(avgvalue);

        priceFloor.value = lower.innerHTML;
        priceCeil.value = upper.innerHTML;
    });
}