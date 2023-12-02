const example = `SELECT accountid Id
FROM account`;
export class QuerySqlPage {
    static initializeEditor(container) {
        let editor;
        require(['vs/editor/editor.main'], function () {
            editor = monaco.editor.create(container, {
                value: example,
                language: 'sql',
                theme: 'vs-dark'
            });
            editor.layout();
            return editor;
        });

        // wait for the editor to be initialized and then return it
        return new Promise((resolve, reject) => {
            let interval = setInterval(() => {
                if (editor) {
                    clearInterval(interval);
                    resolve(editor);
                }
            }, 100);
        });
    }
}