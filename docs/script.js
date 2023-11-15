function showPage(pageId) {
    const iframe = document.getElementById('content-iframe');
    iframe.src = `${pageId}`;
    iframe.style.width = '80%';
    iframe.style.height = '100%';
}