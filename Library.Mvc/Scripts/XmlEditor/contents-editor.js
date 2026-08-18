$(document).ready(function () {

    var initialXml =
        $('#initialContentsXml').val();

    if (initialXml && initialXml.trim() !== '') {
        loadXml(initialXml);
    }

    $('#add-chapter').click(function () {
        addChapter('', '');
    });

    $('form').submit(function () {

        $('#contentsXml').val(
            buildXml()
        );

    });

});


function addChapter(title, page) {

    var row = $('<tr>')
        .addClass('chapter-row');

    var numberCell = $('<td>')
        .addClass('chapter-number');

    var titleCell = $('<td>');

    var titleInput = $('<input>')
        .attr('type', 'text')
        .addClass('form-control chapter-title')
        .val(title);

    var pageCell = $('<td>');

    var pageInput = $('<input>')
        .attr('type', 'number')
        .addClass('form-control chapter-page')
        .val(page);

    var actionsCell = $('<td>');

    var deleteButton = $('<button>')
        .attr('type', 'button')
        .addClass('btn btn-danger btn-sm')
        .text('Удалить');

    titleCell.append(titleInput);
    pageCell.append(pageInput);
    actionsCell.append(deleteButton);

    row.append(numberCell);
    row.append(titleCell);
    row.append(pageCell);
    row.append(actionsCell);

    $('#chapters-container')
        .append(row);

    deleteButton.click(function () {

        if (confirm('Удалить главу?')) {
            row.remove();
            updateChapterNumbers();
        }

    });

    updateChapterNumbers();
}


function updateChapterNumbers() {

    $('#chapters-container .chapter-row')
        .each(function (index) {

            $(this)
                .find('.chapter-number')
                .text(index + 1);

        });
}


function loadXml(xml) {

    if (!xml || xml.trim() === '') {
        return;
    }

    var parser = new DOMParser();

    var documentXml =
        parser.parseFromString(
            xml,
            'text/xml'
        );

    var chapters =
        documentXml.getElementsByTagName(
            'chapter'
        );

    for (var i = 0; i < chapters.length; i++) {

        var chapter = chapters[i];

        var title =
            chapter.getElementsByTagName(
                'title'
            )[0];

        var page =
            chapter.getElementsByTagName(
                'page'
            )[0];

        addChapter(
            title ? title.textContent : '',
            page ? page.textContent : ''
        );
    }
}


function buildXml() {

    var xml = '<contents>';

    $('#chapters-container .chapter-row')
        .each(function (index) {

            var title =
                $(this)
                    .find('.chapter-title')
                    .val();

            var page =
                $(this)
                    .find('.chapter-page')
                    .val();

            xml +=
                '<chapter number="' +
                (index + 1) +
                '">';

            xml +=
                '<title>' +
                escapeXml(title) +
                '</title>';

            xml +=
                '<page>' +
                escapeXml(page) +
                '</page>';

            xml +=
                '</chapter>';
        });

    xml += '</contents>';

    return xml;
}


function escapeXml(value) {

    return String(value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&apos;');
}