import React from 'react';
import PropTypes from 'prop-types';
import Clipboard from "clipboard"

class CopyButton extends React.Component {
    constructor(props) {
        super(props);
        this.state = { showSuccess: false };
    }

    componentDidMount() {
        this.clipboard = new Clipboard(this.element);

        const self = this;
        this.clipboard.on('success', function (e) {
            self.setState({ showSuccess: true });
        });
    }

    onMouseLeave = () => {
        if (this.state.showSuccess) {
            this.setState({ showSuccess: false });
        }
    }

    render() {
        return (
            <div className={`button is-success ${this.state.showSuccess ? 'tooltip' : ''}`}
                data-tooltip="Code copied"
                ref={(element) => { this.element = element; }}
                data-clipboard-text={this.props.value}
                onMouseLeave={this.onMouseLeave}>
                <span className="icon">
                    <i className="fa fa-copy"></i>
                </span>
                <span>Copy to clipboard</span>
            </div>
        );
    }
}

CopyButton.propTypes = {
    value: PropTypes.string,
};

CopyButton.defaultProps = {
    value: "",
};

export default CopyButton;
