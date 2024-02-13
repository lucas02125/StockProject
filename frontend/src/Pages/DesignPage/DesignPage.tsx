import React from "react";
import Table from "../../Components/Table/Table";
import RatioList from "../../Components/RatioList/RatioList";
import { testIncomeStatementData } from "../../Components/Table/testData";
import { config } from "dotenv";

type Props = {};

const tableConfig = [
  {
    label: "symbol",
    render: (company: any) => company.marketCapTTM,
    subtitle: "THis is the subtitle of the design page",
  },
];

const DesignPage = (props: Props) => {
  return (
    <>
      <h1>DesignPage</h1>
      <h2>This is a design page for you</h2>
      <RatioList data={testIncomeStatementData} config={tableConfig} />
      <Table data={testIncomeStatementData} config={tableConfig} />
    </>
  );
};

export default DesignPage;
