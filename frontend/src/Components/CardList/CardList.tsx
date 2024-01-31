import React from "react";
import Card from "../Card/Card";

interface Props {}

const CardList: React.FC<Props> = (props: Props): JSX.Element => {
  return (
    <div>
      <Card companyName="Apple" ticker="APPL" price={200} />
      <Card companyName="Microsoft" ticker="MCSFT" price={350} />
      <Card companyName="Tesla" ticker="TSLA" price={500} />
    </div>
  );
};

export default CardList;
