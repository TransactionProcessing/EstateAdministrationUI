if (typeof module !== "undefined" && module.exports)
{
    module.exports.getAlertHtml = getAlertHtml;
}

function getAlertHtml(type,
                      title,
                      message) {
    var userMessageArray = message.split('|');

    var userMessage = null;
    if (userMessageArray.length > 1)
    {
        userMessage = userMessageArray[1];
    }
    else
    {
        userMessage = userMessageArray[0];
    }

    var html = '<div class="alert alert-' + type + ' alert-dismissible" role="alert">';
    html = html + '<button type="button" class="close" data-dismiss="alert" aria-label="Close">';
    html = html + '<span aria-hidden="true">&times;</span>';
    html = html + '</button>';
    html = html + '<strong>' + title + '</strong> ' + userMessage + '</div>';

    console.log(html);
    return html;
}