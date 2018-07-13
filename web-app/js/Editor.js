import React from 'react';
import MonacoEditor from 'react-monaco-editor';
import PropTypes from 'prop-types';
import ReactResizeDetector from 'react-resize-detector';


class Editor extends React.Component {
    editor = null;

    constructor(props) {
        super(props);
    }

    editorDidMount = (editor, monaco) => {
        this.props.editorDidMount();
        this.editor = editor;
    }

    onChange = (newValue, e) => {
        this.props.onChange(newValue);
    }

    onResize = () => {
        if (this.editor !== null)
            this.editor.layout();
    }

    render() {
        const options = {
            selectOnLineNumbers: true,
            lineNumbers: false,
            readOnly: this.props.isReadOnly,
            minimap: {
                enabled: false
            }
        };
        const requireConfig = {
            url:  "libs/requirejs/require.js",
            paths: {
                vs: "libs/vs"
            }
        };
        return (
            <div style={{height: '100%', overflow: 'hidden'}}>
                <ReactResizeDetector handleWidth handleHeight onResize={this.onResize} />
                <MonacoEditor
                    language={this.props.language}
                    value={this.props.value}
                    options={options}
                    onChange={this.onChange}
                    editorDidMount={this.editorDidMount}
                    requireConfig={requireConfig}
                />
            </div>
        );
    }
}

function noop() { }

Editor.propTypes = {
    onChange: PropTypes.func,
    value: PropTypes.string,
    language: PropTypes.string,
    isReadOnly: PropTypes.bool,
    editorDidMount: PropTypes.func
};

Editor.defaultProps = {
    onChange: noop,
    value: "",
    language: "html",
    isReadOnly: false,
    editorDidMount: noop
};

export default Editor;
