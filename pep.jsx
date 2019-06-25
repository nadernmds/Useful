import React, { Component } from 'react';
import Reactable from 'reactable';
import './pep.css'
var Table = Reactable.Table,
    Thead = Reactable.Thead,
    Th = Reactable.Th;
var sgTeams = [
    { name: "SG-1", leader: "نادر محمودی", assignment: "Exploration", members: 4 },
    { name: "SG-2", leader: "Kawalsky", assignment: "Search and Rescue", members: 5 },
    { name: "SG-3", leader: "Reynolds", assignment: "Marine Combat", members: 10 },
    { name: "SG-4", leader: "Howe", assignment: "Medical", members: 4 },
    { name: "SG-5", leader: "Davis", assignment: "Marine Combat", members: 6 },
    { name: "SG-6", leader: "Fischer", assignment: "Search and Rescue", members: 10 },
    { name: "SG-7", leader: "Isaacs", assignment: "Scientific", members: 6 },
    { name: "SG-8", leader: "Yip", assignment: "Medical", members: 6 },
    { name: "SG-9", leader: "Winters", assignment: "Diplomatic", members: 7 },
    { name: "SG-10", leader: "Colville", assignment: "Military Exploration", members: 5 }
];
class Pep extends Component {
    state = {}
    render() {
        return (
        <Table 
            className="table"
            filterable={['leader', 'assignment', 'members']}
            noDataText="No matching records found"
            itemsPerPage={2}
            currentPage={0}
            sortable={true}
            nextPageLabel='قبلی'
            previousPageLabel='بعدی'
            data={sgTeams}>
            <Thead>
                <Th column="name">نام</Th>
                <Th column="leader">رییس</Th>
                <Th column="assignment">Mission</Th>
                <Th column="members">Team Members</Th>
            </Thead>
        </Table>
        
        );
    }
}

export default Pep;