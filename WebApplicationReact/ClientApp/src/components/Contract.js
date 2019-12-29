
import React, { Component } from 'react';
import ReactTable from 'react-table';
import "react-table/react-table.css";
import Modal from 'react-bootstrap/Modal';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import { Formik, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { Form, Datepicker } from 'react-formik-ui'

class Contract extends Component {
    static displayName = Contract.name;

    constructor(props) {
        super(props);
        this.state = {
            data: [],
            loading: false,
            pages: -1,
            currentSort: null,
            currentFilter: null,
            modalEditIsOpen: false,
            modalAddIsOpen: false,
            modalDeleteIsOpen: false,
            currentID: '',
            currentData: null
        };

        this.openEditModal = this.openEditModal.bind(this);
        this.closeEditModal = this.closeEditModal.bind(this);
        this.closeEditModalNoSave = this.closeEditModalNoSave.bind(this);

        this.openAddModal = this.openAddModal.bind(this);
        this.afterOpenAddModal = this.afterOpenAddModal.bind(this);
        this.closeAddModal = this.closeAddModal.bind(this);

        this.openDeleteModal = this.openDeleteModal.bind(this);
        this.afterOpenDeleteModal = this.afterOpenDeleteModal.bind(this);
        this.closeDeleteModal = this.closeDeleteModal.bind(this);

        this.setValue = this.setValue.bind(this);
    }

    formatDate(d) {
        if (d == null || d == 'undefined') {
            return ""
        }
        let date = new Date(d)
        const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        return date.getDate().toString() + "/" + months[date.getMonth()] + "/" + date.getFullYear().toString()
    }

    formatDateForEdit(d) {
        const date = new Date(d)
        // const frmDate = (new Intl.DateTimeFormat("en-GB", { year: 'numeric', month: 'short', day: '2-digit' } )).format(date)
        const dateAsString = date.getFullYear().toString() + "/" + (date.getMonth() + 1).toString() + "/" + date.getDate().toString()
        return dateAsString
    }

    formatAmount(a) {
        const dp = 2
        var w = a.toFixed(dp), k = w | 0, b = a < 0 ? 1 : 0,
            u = Math.abs(w - k), d = ('' + u.toFixed(dp)).substr(2, dp),
            s = '' + k, i = s.length, r = '';
        while ((i -= 3) > b) { r = ',' + s.substr(i, 3) + r; }
        return "\u20AC " + s.substr(0, i + 3) + r + (d ? '.' + d : '');
    }

    dateSorter(a, b, desc) {
        // force null and undefined to the bottom
        a = a === null || a === undefined ? -Infinity : a
        b = b === null || b === undefined ? -Infinity : b

        let aDate = new Date(a)
        let bDate = new Date(b)

        if (aDate > bDate) {
            return desc ? -1 : 1
        }
        if (aDate < bDate) {
            return desc ? 1 : -1
        }
        // returning 0 or undefined will use any subsequent column sorting methods or the row index as a tiebreaker
        return 0
    }

    downloadToExcel = () => {
        fetch('/api/home/getcontractdataexport', {
            method: 'post',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                page: 1,
                pageSize: this.state.pageSize,
                sorted: this.state.currentSort,
                filtered: this.state.currentFilter
            })
        })
            .then(response => {
                response.blob().then(blob => {
                    let url = window.URL.createObjectURL(blob);
                    let a = document.createElement('a');
                    let filename = "Contractdata - " + this.formatDate(Date.now()).replace(/\//g, "-") + ".xlsx";
                    a.href = url;
                    a.download = filename;
                    a.click();
                    a.remove();
                });
            });
    }

    openEditModal(row) {
        fetch('/api/home/getonecontractdata', {
            method: 'post',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ID: row.row.id
            })
        })
            .then(response => response.json())
            .then((res) => {
                // Update form values
                // currentData: { ...res, startdate: this.formatDateForEdit(res.startdate), enddate: this.formatDateForEdit(res.enddate) },
                this.setState({
                    currentData: { ...res },
                    currentID: row.row.id,
                    modalEditIsOpen: true
                })
            })
            .catch(e => console.error(e))
    }

    closeEditModal(e) {
        // update the record
        fetch('/api/home/putonecontractdata', {
            method: 'post',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                { ...this.state.currentData })
        }
        )
            .then(response => response.json())
            .then((res) => {
                alert(res.statusCode.toString());
                e.preventDefault();
            })
            .catch(e => console.error(e))

        this.setState({ modalEditIsOpen: false, currentID: '', currentData: null });
    }

    closeEditModalNoSave() {
        this.setState({ modalEditIsOpen: false, currentID: '', currentData: null });
    }

    openAddModal() {
        this.setState({ modalAddIsOpen: true });
    }

    afterOpenAddModal() {
        // references are now sync'd and can be accessed.
    }

    closeAddModal() {
        this.setState({ modalAddIsOpen: false });
    }

    openDeleteModal() {
        this.setState({ modalDeleteIsOpen: true });
    }

    afterOpenDeleteModal() {
        // references are now sync'd and can be accessed.
    }

    closeDeleteModal() {
        this.setState({ modalDeleteIsOpen: false });
    }

    setValue(property, value) {
        let cd = this.state.currentData;
        cd[property] = value;

        this.setState({ currentData: cd });
    }

    //
    // w3-styles are used to have properly behaving dropdown menu in the react-table 
    //
    render() {
        const columns = [
            {
                Header: 'Actions',
                Cell: row => (
                    <div className="w3-bar">
                        <button className="w3-bar-item w3-button" title="Details" >
                            <i className="fa fa-file-text-o" ></i>
                        </button>
                        <button className="w3-bar-item w3-button" title="Edit" onClick={() => { this.openEditModal(row); }}>
                            <i className="fa fa-pencil" ></i>
                        </button>
                        <button className="w3-bar-item w3-button" title="Delete" onClick={() => { this.openDeleteModal(row); }}>
                            <i className="fa fa-trash-o" ></i>
                        </button>
                        <div className="w3-dropdown-hover">
                            <button className="w3-button" title="More actions...">...</button>
                            <div className="w3-dropdown-content w3-bar-block w3-card-4">
                                <a href="#" className="w3-bar-item w3-button" >Link 1</a>
                                <a href="#" className="w3-bar-item w3-button">Link 2</a>
                                <a href="#" className="w3-bar-item w3-button">Link 3</a>
                            </div>
                        </div>
                    </div>
                )
            },
            {
                Header: 'ID',
                accessor: 'id',
                show: false
            },
            {
                Header: 'Code',
                accessor: 'code'
            },
            {
                Header: 'Title',
                accessor: 'title'
            },
            {
                Header: 'Description',
                accessor: 'description'
            },
            {
                id: 'value',
                Header: 'Value',
                accessor: d => this.formatAmount(d.value),
                style: { 'textAlign': "right" }
            },
            {
                id: 'startdate',
                Header: 'Start date',
                accessor: d => this.formatDate(d.startdate),
                // date sorting
                sortMethod: this.dateSorter
            },
            {
                id: 'enddate',
                Header: 'End date',
                accessor: d => this.formatDate(d.enddate),
                // date sorting
                sortMethod: this.dateSorter
            }
        ];
        const paymentcolumns = [
            {
                Header: 'ID',
                accessor: 'id',
                show: false
            },
            {
                Header: 'Description',
                accessor: 'description'
            },
            {
                id: 'plannedinvoicedate',
                Header: 'Planned date',
                accessor: d => this.formatDate(d.plannedinvoicedate),
                // date sorting
                sortMethod: this.dateSorter
            },
            {
                id: 'actualinvoicedate',
                Header: 'Actual date',
                accessor: d => this.formatDate(d.actualinvoicedate),
                // date sorting
                sortMethod: this.dateSorter
            },
            {
                id: 'amount',
                Header: 'Amount',
                accessor: d => this.formatAmount(d.amount),
                style: { 'textAlign': "right" }
            }
        ];

        return (
            <div>
                <button className="w3-button w3-light-grey w3-round" onClick={this.openAddModal} title="Add new record">
                    <i className="fa fa-plus-circle" ></i>&nbsp;Add new
                </button>
                <ReactTable className="-striped"
                    data={this.state.data}
                    pages={this.state.pages}
                    loading={this.state.loading}
                    manual
                    filterable
                    minRows={1}
                    onFetchData={(state, instance) => {
                        // show the loading overlay
                        this.setState({ loading: true })
                        // fetch your data

                        fetch('/api/home/getcontractdata', {
                            method: 'post',
                            headers: {
                                'Accept': 'application/json, text/plain, */*',
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify({
                                page: state.page,
                                pageSize: state.pageSize,
                                sorted: state.sorted,
                                filtered: state.filtered
                            })
                        })
                            .then(response => response.json())
                            .then((res) => {
                                // Update react-table
                                this.setState({
                                    data: res.rows,
                                    pages: res.pages,
                                    loading: false,
                                    currentFilter: state.filtered,
                                    currentSort: state.sorted
                                })
                            })
                            .catch(e => console.error(e))
                    }}
                    columns={columns}
                />
                <p />
                <button className="w3-button w3-light-grey w3-round" onClick={this.downloadToExcel} title="Export to Excel" float="right">
                    <i className="fa fa-file-excel-o" ></i>&nbsp;Export
                </button>

                <Modal show={this.state.modalEditIsOpen} onHide={this.closeEditModalNoSave}  size='xl' >
                    <Modal.Header closeButton >
                        <Modal.Title>Edit Contract</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Tabs defaultActiveKey='details' id='detailstab'>
                            <Tab eventKey='details' title='Details'>
                                <Formik
                                    initialValues={this.state.currentData}
                                    enableReinitialize={true}
                                    onSubmit={(values, { setSubmitting }) => {
                                        setTimeout(() => {
                                            alert(JSON.stringify(values, null, 2));
                                            setSubmitting(false);
                                            this.closeEditModalNoSave();
                                        }, 400);

                                    }}
                                >
                                    <Form className="w3-container">
                                        <label id="lID"  >ID: </label>
                                        <Field name="id" type="number" className="w3-input w3-border" disabled />
                                        <label id="lCode"  >Code: </label>
                                        <Field name="code" type="text" className="w3-input w3-border" />
                                        <label id="lTitle"  >Title: </label>
                                        <Field name="title" type="text" className="w3-input w3-border" />
                                        <label id="lDescription"  >Description: </label>
                                        <Field name="description" type="text" className="w3-input w3-border" />
                                        <label id="lStartdate" >Start/End date: </label>
                                        <div className="w3-cell-row">
                                            <div className="w3-cell">
                                                <Datepicker name="startdate" className="w3-input w3-border" >
                                                </Datepicker>
                                            </div>
                                            <div className="w3-cell">
                                                <Datepicker name="enddate" className="w3-input w3-border" >
                                                </Datepicker>
                                            </div>
                                        </div>
                                        <label id="lValue"  >Value: </label>
                                        <Field name="value" type="number" className="w3-input w3-border" />
                                        <hr />
                                        <div className="w3-bar">
                                            <button type="submit" className="w3-button w3-light-grey w3-round" title="Saves this record" >
                                                <i className="fa fa-save" ></i>&nbsp;Save
                                </button>
                                        </div>
                                    </Form>

                                </Formik>
                            </Tab>
                            <Tab eventKey="payments" title="Payments">
                                <div>
                                    <button className="w3-button w3-light-grey w3-round" onClick={this.openAddModal} title="Add new record">
                                        <i className="fa fa-plus-circle" ></i>&nbsp;Add new
                                    </button>
                                    {this.state.currentData && (
                                        <ReactTable className="-striped"
                                            data={this.state.currentData.paymentInfo}
                                            loading={this.state.loading}
                                            minRows={1}
                                            columns={paymentcolumns}
                                        />)
                                    }
                                    <p />
                                    <button className="w3-button w3-light-grey w3-round" onClick={this.downloadToExcel} title="Export to Excel" float="right">
                                        <i className="fa fa-file-excel-o" ></i>&nbsp;Export
                                    </button>
                                </div>


                            </Tab>
                        </Tabs>
                    </Modal.Body>
                </Modal>

                <Modal show={this.state.modalAddIsOpen} onHide={this.closeAddModal}>
                    <Modal.Header closeButton >
                        <Modal.Title>Add Contract</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Formik
                            initialValues={{ code: "", description: "", title: "", startdate: new Date(), enddate: new Date(), value: 0.0 }}
                            enableReinitialize={true}
                            onSubmit={(values, { setSubmitting }) => {
                                setTimeout(() => {
                                    alert(JSON.stringify(values, null, 2));
                                    setSubmitting(false);
                                    this.closeEditModalNoSave();
                                }, 400);
                            }}
                        >
                            <Form className="w3-container">
                                <label id="lCode"  >Code: </label>
                                <Field name="code" type="text" className="w3-input w3-border" />
                                <label id="lTitle"  >Title: </label>
                                <Field name="title" type="text" className="w3-input w3-border" />
                                <label id="lDescription"  >Description: </label>
                                <Field name="description" type="text" className="w3-input w3-border" />
                                <label id="lStartdate" >Start/End date: </label>
                                <div className="w3-cell-row">
                                    <div className="w3-cell">
                                        <Datepicker name="startdate" className="w3-input w3-border" >
                                        </Datepicker>
                                    </div>
                                    <div className="w3-cell">
                                        <Datepicker name="enddate" className="w3-input w3-border" >
                                        </Datepicker>
                                    </div>
                                </div>
                                <label id="lValue"  >Value: </label>
                                <Field name="value" type="number" className="w3-input w3-border" />
                                <hr />
                                <div className="w3-bar">
                                    <button type="submit" className="w3-button w3-light-grey w3-round" title="Saves this record" >
                                        <i className="fa fa-save" ></i>&nbsp;Save
                                </button>
                                </div>
                            </Form>
                        </Formik>
                    </Modal.Body>

                </Modal>

                <Modal show={this.state.modalDeleteIsOpen} onHide={this.closeDeleteModal}>
                    <Modal.Header closeButton >
                        <Modal.Title>Delete Contract</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                    </Modal.Body>
                </Modal>
            </div>
        );
    }
}

// required to have lazy loading
export default Contract;
