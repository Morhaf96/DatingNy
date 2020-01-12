(function () {
    function messageToHtml(message, className) {
        return `<article class="message ${className}">
                    <header class="header">
                        <time class="date">${message.Timestamp}</time>
                        <h2>${message.UserName}</h2>
                    </header>
                    <main class="body">${message.Message}</main>
                </article>`;
    }
    function updateMessageList() {
        // Hämta användarid från den dolda input-taggen:
        const userId = $('#user-id').val();

        $.get('/api/postmessageapi/list')
            .then((resp) => {
                if (resp && Array.isArray(resp)) {
                    $('#messagelist').html('');
                    resp.forEach((mess) => {
                        const isMine = mess.UserId === userId;
                        $('#messagelist').append(
                            messageToHtml(mess, isMine ? 'own-message' : 'other-message')
                        );
                    });
                }
            });
    }

    function sendMessage() {
        const newMessage = $('#new-message').val();
        const timestamp = new Date().toISOString();
        const userId = $('#user-id').val();

        if (newMessage) {
            const messageObj = {
                Message: newMessage,
                Timestamp: timestamp,
                UserId: userId
            };
            $.post('/api/postmessageapi/send', messageObj)
                .then((resp) => {
                    if (resp === "Ok") {
                        $('#new-message').val('');
                        updateMessageList();
                    } else {
                        alert('Något gick fel!');
                    }
                });
        }
    }

    window.addEventListener('load', () => {
        updateMessageList();

        if ($('#user-id').length > 0) {
            $('#send-button').click(sendMessage);
        }
    });
})();
