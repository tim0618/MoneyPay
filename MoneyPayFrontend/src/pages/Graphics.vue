<template>
    <div class="q-pa-md">
        <div class="card">
            <div class="card-title">本月收支概況</div>
            <div class="chart-container">
                <apexchart type="bar" height="300" :options="balanceChartOptions" :series="balanceChartSeries">
                </apexchart>
            </div>
        </div>

        <div class="card">
            <div class="card-title">支出分類佔比</div>

            <div v-if="moneyStore.totalExpense === 0" class="no-data">
                還沒有支出紀錄喔！快去首頁記帳吧。
            </div>

            <div v-else class="chart-container">
                <apexchart type="donut" height="350" :options="expenseChartOptions" :series="expenseChartSeries">
                </apexchart>
            </div>
        </div>
    </div>
</template>

<script setup>
import { computed, onMounted } from "vue";
import { useMoneyStore } from "../stores/moneyStore";

const moneyStore = useMoneyStore();

onMounted(() => {
    // 確保有點資料，不然圖表是空的
    moneyStore.initData();
});

// ==========================================
// 1. 收支對比圖 (Bar Chart) 設定
// ==========================================
const balanceChartSeries = computed(() => {
    return [{
        name: '金額',
        data: [moneyStore.totalIncome, moneyStore.totalExpense]
    }];
});

const balanceChartOptions = computed(() => {
    return {
        chart: {
            id: 'balance-chart',
            toolbar: { show: false },
            fontFamily: 'Helvetica, Arial, sans-serif', // 設定字體讓整體更乾淨
        },
        // ★ 修改這裡：使用柔和的色碼
        // #64B5F6 (柔和藍 - 收入)
        // #E57373 (柔和紅 - 支出)
        colors: ['#64B5F6', '#E57373'],

        // ★ 額外建議：加上透明度讓顏色更通透
        fill: {
            opacity: 0.9
        },
        states: {
            hover: {
                filter: {
                    type: 'darken',
                    value: 0.85
                }
            },
            active: {
                allowMultipleDataPointsSelection: false,
                filter: {
                    type: 'none',
                }
            }
        },

        plotOptions: {
            bar: {
                borderRadius: 6, // 圓角大一點看起來比較可愛/現代
                horizontal: true,
                distributed: true,
                barHeight: '60%', // 讓 Bar 不要太肥，看起來比較優雅
            }
        },
        dataLabels: {
            enabled: true,
            style: {
                fontSize: '14px',
                fontWeight: 'bold',
                colors: ['#fff'] // 文字維持白色
            }
        },
        xaxis: {
            categories: ['總收入', '總支出'],
            labels: {
                style: { colors: '#666' } // 軸線文字改深灰色，不要純黑
            }
        },
        grid: {
            borderColor: '#f1f1f1' // 格線淡一點
        },
        legend: { show: false }
    };
});

// ==========================================
// 2. 支出圓餅圖 (Donut Chart) 設定
// ==========================================

// 資料來源：只取支出 (Expense) 且金額大於 0 的項目
const activeExpenses = computed(() => {
    return moneyStore.expenseList.filter(item => item.totalPay > 0);
});

const expenseChartSeries = computed(() => {
    // Series 只需要金額的陣列：[432, 546, 500...]
    return activeExpenses.value.map(item => item.totalPay);
});

const expenseChartOptions = computed(() => {
    const labels = activeExpenses.value.map(item => item.typeName);
    const colors = activeExpenses.value.map(item => item.color);

    return {
        chart: {
            type: 'donut',
            fontFamily: 'Helvetica, Arial, sans-serif',
        },
        labels: labels,

        // 1. 這裡維持使用你原本 Mock Data 的顏色
        colors: colors,

        // 2. ★ 關鍵修改：降低填充的不透明度
        // 1.0 是全實色(最亮)，建議設 0.7 ~ 0.8
        // 這樣顏色會透白，看起來就會變柔和，不會那麼刺眼
        fill: {
            opacity: 0.8
        },
        states: {
            hover: {
                filter: {
                    type: 'darken',
                    value: 0.85
                }
            },
            active: {
                allowMultipleDataPointsSelection: false,
                filter: {
                    type: 'none',
                }
            }
        },

        // 3. ★ 建議加上白色邊框
        // 這能把顏色區塊隔開，視覺上會更乾淨，減少顏色擠在一起的壓迫感
        stroke: {
            show: true,
            colors: ['#fff'],
            width: 2
        },

        plotOptions: {
            pie: {
                donut: {
                    size: '75%', // 甜甜圈中間挖大一點，也會減少色塊面積，比較清爽
                    labels: {
                        show: true,
                        name: {
                            show: true,
                            fontSize: '14px',
                            fontWeight: 600,
                            color: '#888',
                            offsetY: -10
                        },
                        value: {
                            show: true,
                            fontSize: '24px',
                            fontWeight: 700,
                            color: '#333',
                            offsetY: 10,
                            formatter: (val) => `$${val}`
                        },
                        total: {
                            show: true,
                            showAlways: true,
                            label: '總支出',
                            fontSize: '14px',
                            fontWeight: 600,
                            color: '#888',
                            formatter: () => `$${moneyStore.totalExpense}`
                        }
                    }
                }
            }
        },
        dataLabels: { enabled: false },
        legend: {
            position: 'bottom',
            markers: {
                radius: 12 // 圖例變圓形比較可愛
            }
        }
    };
});
</script>

<style scoped>
/* 沿用你的卡片風格 */
.card {
    background: white;
    border-radius: 12px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
    margin-bottom: 20px;
    padding: 20px;
    overflow: hidden;
}

.card-title {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 15px;
    color: #333;
    border-left: 4px solid #FF7043;
    /* 裝飾線 */
    padding-left: 10px;
}

.chart-container {
    display: flex;
    justify-content: center;
    align-items: center;
}

.no-data {
    text-align: center;
    color: #999;
    padding: 40px 0;
}
</style>