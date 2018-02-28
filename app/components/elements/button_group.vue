<template>
  <span :is="container">
    <span class="shadow2">
      <Btn
        v-for="option in options"
        :key="option[idField]"
        :class="value === option[idField] ? 'btn-primary' : 'bg-white black'"
        :label="option[labelField]"
        class="not-rounded"
        @click="$emit('input', option[idField])" />
    </span>
  </span>
</template>

<script>
  export default {
    name: "ButtonGroup",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
  };
</script>
