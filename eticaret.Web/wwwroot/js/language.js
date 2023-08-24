function googleTranslateElementInit2() { new google.translate.TranslateElement({ pageLanguage: 'tr', autoDisplay: false }, 'google_translate_element'); }

jQuery('.switcher .selected').click(function () {
    if (!(jQuery('.switcher .option').is(':visible'))) {
        jQuery('.switcher .option').stop(true, true).delay(100).slideDown(500);
        jQuery('.switcher .selected a').toggleClass('open')
    }
});


jQuery('.switcher .option').bind('mousewheel', function (e) {
    var options = jQuery('.switcher .option');
    if (options.is(':visible')) options.scrollTop(options.scrollTop() - e.originalEvent.wheelDelta); return false;
});
jQuery('body').not('.switcher').mousedown(function (e) {
    if (jQuery('.switcher .option').is(':visible') && e.target != jQuery('.switcher .option').get(0)) {
        jQuery('.switcher .option').stop(true, true).delay(100).slideUp(500);
        jQuery('.switcher .selected a').toggleClass('open')
    }
}); 

/*-----------------------------------------------------------------*/

function GTranslateGetCurrentLang() { var keyValue = document.cookie.match('(^|;) ?googtrans=([^;]*)(;|$)'); return keyValue ? keyValue[2].split('/')[2] : null; }
function GTranslateFireEvent(element, event) { try { if (document.createEventObject) { var evt = document.createEventObject(); element.fireEvent('on' + event, evt) } else { var evt = document.createEvent('HTMLEvents'); evt.initEvent(event, true, true); element.dispatchEvent(evt) } } catch (e) { } }
function doGTranslate(lang_pair) { if (lang_pair.value) lang_pair = lang_pair.value; if (lang_pair == '') return; var lang = lang_pair.split('|')[1]; if (GTranslateGetCurrentLang() == null && lang == lang_pair.split('|')[0]) return; var teCombo; var sel = document.getElementsByTagName('select'); for (var i = 0; i < sel.length; i++)if (sel[i].className == 'goog-te-combo') teCombo = sel[i]; if (document.getElementById('google_translate_element') == null || document.getElementById('google_translate_element').innerHTML.length == 0 || teCombo.length == 0 || teCombo.innerHTML.length == 0) { setTimeout(function () { doGTranslate(lang_pair) }, 500) } else { teCombo.value = lang; GTranslateFireEvent(teCombo, 'change'); GTranslateFireEvent(teCombo, 'change') } }
if (GTranslateGetCurrentLang() != null) jQuery(document).ready(function () { jQuery('div.switcher div.selected a').html(jQuery('div.switcher div.option').find('img[alt="' + GTranslateGetCurrentLang() + '"]').parent().html()); });

 

function googleTranslateElementInit2() { new google.translate.TranslateElement({ pageLanguage: 'tr', autoDisplay: false }, 'google_translate_element'); }