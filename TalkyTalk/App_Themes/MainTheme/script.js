document.addEventListener('DOMContentLoaded', function () {
    const postDescElements = document.querySelectorAll('.post-desc');
    const maxLength = 380;
    postDescElements.forEach(function (postDesc) {
        const text = postDesc.textContent.trim();
        if (text.length > maxLength) {
            const truncatedText = text.substring(0, maxLength).trim();
            postDesc.textContent = `${truncatedText}.......`;
        }
    });

    var editBtn = document.getElementById('<%= EdiCommenttBtn.ClientID %>');
    var deleteBtn = document.getElementById('<%= DeleteCommentBtn.ClientID %>');

    var reportCommentDiv = document.getElementById('report-comment');

        if (editBtn && deleteBtn) {
        reportCommentDiv.style.display = 'none';
    }
});
