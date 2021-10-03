import React from "react";
import moment from 'moment'
import 'moment-timezone';
const MemberItemComponent = (props) => {
  const { member } = props;
  return (
    <tr key={member.id}>
      <td>{member.name}</td>
      <td>{member.lastName}</td>
      <td>{member.title}</td>
      <td>{moment(member.leavingStartDate).format("DD-MM-YYYY")}</td>
      <td>{moment(member.leavingEndDate).format("DD-MM-YYYY")}</td>
    </tr>
  );
};
export default MemberItemComponent;